using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ZM.Application.Dependencies.Infrastructure.Authentication;
using ZM.Application.Dependencies.Infrastructure.Persistence;
using ZM.Domain.Chats;
using ZM.Domain.Entities;

namespace ZM.Api.Hubs;

/// <summary>
/// Хаб чата.
/// </summary>
[Authorize]
public class P2PChatHub(IDbContext _dbContext, TimeProvider _timeProvider) : Hub
{
    /// <summary>
    /// Текущие пользователи подключенные к хабу (т.е. онлайн). Key: externalId, Value: connectionId
    /// </summary>
    private readonly static Dictionary<Guid, string> _onlineUsers = [];

    public override async Task OnConnectedAsync()
    {
        var userExternalId = GetCurrentExternalId();

        var isUserConnected = _onlineUsers.ContainsKey(userExternalId);

        if (!isUserConnected)
        {
            _onlineUsers.Add(userExternalId, Context.ConnectionId);
        }

        await base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        var userExternalId = GetCurrentExternalId();
        _onlineUsers.Remove(userExternalId);

        return base.OnDisconnectedAsync(exception);
    }

    //TODO: Добавить проверки.
    public async Task Send(string chatId, string content)
    {
        if (string.IsNullOrEmpty(content))
            return;

        var gChatId = Guid.Parse(chatId);

        var senderExternalId = GetCurrentExternalId();

        if (_onlineUsers.TryGetValue(senderExternalId, out string connectionId))
        {
            var sender = await _dbContext.Set<User>().SingleOrDefaultAsync(u => u.ExternalId == senderExternalId);
            var chat = await _dbContext.Set<P2PChat>()
                .Include(ch => ch.Users)
                .SingleOrDefaultAsync(ch => ch.Id == gChatId);

            var chatMessage = new P2PChatMessage(content, _timeProvider.GetUtcNow().UtcDateTime, sender.Id, chat.Id);
            await _dbContext.Set<P2PChatMessage>().AddAsync(chatMessage);
            await _dbContext.SaveChangesAsync();

            var interlocutorExternalId = chat.Users.First(u => u.ExternalId != senderExternalId).ExternalId;

            var interlocutorOnline = _onlineUsers.TryGetValue(interlocutorExternalId, out var interlocutorConnectionId);

            if (interlocutorOnline)
                await Clients
                    .Client(interlocutorConnectionId!)
                    .SendAsync("newChatMessage",new SendMessageToInterlocutorDto(content, chatMessage.CreatedDate));
        }
    }

    private Guid GetCurrentExternalId()
    {
        return Guid.Parse(Context.User!.Claims.First(c => c.Type == KnownClaims.ExternalId).Value);
    }
}

/// <summary>
/// Сообщение для отправки собеседнику.
/// </summary>
/// <param name="Content">Контент.</param>
/// <param name="CreatedDate">Дата создания.</param>
public record SendMessageToInterlocutorDto(string Content, DateTime CreatedDate);
