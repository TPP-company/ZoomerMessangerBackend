using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using ZM.Application.Dependencies.Infrastructure.Persistence;
using ZM.Domain.Chats;
using ZM.Application.Dependencies.Infrastructure.DataAccess.Common.Queries;
using ZM.Domain.Users;

namespace ZM.Api.Hubs;

/// <summary>
/// Хаб чата.
/// </summary>
[Authorize]
public class P2PChatHub(IDbContext _dbContext, TimeProvider _timeProvider) : Hub
{
	/// <summary>
	/// Текущие пользователи подключенные к хабу (т.е. онлайн). Key: userId, Value: connectionId
	/// </summary>
	private static readonly Dictionary<Guid, string> _onlineUsers = [];

	public override async Task OnConnectedAsync()
	{
		var userId = GetCurrentUserId();

		var isUserConnected = _onlineUsers.ContainsKey(userId);

		if (!isUserConnected)
		{
			_onlineUsers.Add(userId, Context.ConnectionId);
		}

		await base.OnConnectedAsync();
	}

	public override Task OnDisconnectedAsync(Exception exception)
	{
		var userId = GetCurrentUserId();
		_onlineUsers.Remove(userId);

		return base.OnDisconnectedAsync(exception);
	}

	//TODO: Добавить проверки.
	public async Task Send(string chatId, string content)
	{
		if (string.IsNullOrEmpty(content))
			return;

		var gChatId = Guid.Parse(chatId);

		var senderId = GetCurrentUserId();

		if (_onlineUsers.TryGetValue(senderId, out string connectionId))
		{
			var sender = await _dbContext.Set<User>().GetByIdAsync(senderId, default);
			var chat = await _dbContext.Set<P2PChat>()
				.Include(ch => ch.Members)
				.SingleOrDefaultAsync(ch => ch.Id == gChatId);

			var chatMessage = new P2PChatMessage(content, _timeProvider.GetUtcNow().UtcDateTime, sender.Id, chat.Id);
			await _dbContext.Set<P2PChatMessage>().AddAsync(chatMessage);
			await _dbContext.SaveChangesAsync();

            var interlocutorId = chat.Members.First(u => u.Id != senderId).Id;

			var interlocutorOnline = _onlineUsers.TryGetValue(interlocutorId, out var interlocutorConnectionId);

			if (interlocutorOnline)
				await Clients
					.Client(interlocutorConnectionId!)
					.SendAsync("newChatMessage", new SendMessageToInterlocutorDto(content, chatMessage.CreatedDate));
		}
	}

	private Guid GetCurrentUserId()
	{
		return Guid.Parse(Context.User!.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value);
	}
}

/// <summary>
/// Сообщение для отправки собеседнику.
/// </summary>
/// <param name="Content">Контент.</param>
/// <param name="CreatedDate">Дата создания.</param>
public record SendMessageToInterlocutorDto(string Content, DateTime CreatedDate);
