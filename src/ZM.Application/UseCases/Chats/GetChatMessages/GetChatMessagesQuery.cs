using MediatR;
using Microsoft.EntityFrameworkCore;
using ZM.Application.Common.Exceptions;
using ZM.Application.Dependencies.Infrastructure.Authentication;
using ZM.Application.Dependencies.Infrastructure.Persistence;
using ZM.Common.Results;
using ZM.Domain.Chats;

namespace ZM.Application.UseCases.Chats.GetChatMessages;

/// <summary>
/// Получить сообщения чата.
/// </summary>
/// <param name="ChatId">Идентификатор чата.</param>
public record GetChatMessagesQuery(Guid ChatId) : IRequest<Result<IReadOnlyCollection<ChatMessageDto>>>;

//TODO: пагинационное получение истории
public class GetChatMessagesQueryHandler(IDbContext _dbContext, ICurrentUser _currentUser) 
    : IRequestHandler<GetChatMessagesQuery, Result<IReadOnlyCollection<ChatMessageDto>>>
{
    public async Task<Result<IReadOnlyCollection<ChatMessageDto>>> Handle(GetChatMessagesQuery request, CancellationToken cancellationToken)
    {
        var chatExists = await _dbContext.Set<Chat>()
            .SingleOrDefaultAsync(ch => ch.Users.Any(u => u.ExternalId == _currentUser.ExternalId), cancellationToken)
            ?? throw new ResourceNotFoundException();

        var chatMessages = await _dbContext.Set<ChatMessage>()
            .Where(m => m.ChatId == request.ChatId)
            .OrderBy(m => m.CreatedDate)
            .Select(m => new ChatMessageDto
            {
                Id = m.Id,
                Content = m.Content,
                CreatedDate = m.CreatedDate,
                HasBeenRead = m.HasBeenRead,
                Sender = new SenderDto
                {
                    Id = m.Sender.Id,
                    UserName = m.Sender.UserName,
                }
            })
            .ToListAsync(cancellationToken);

        return chatMessages;
    }
}

/// <summary>
/// Сообщение чата.
/// </summary>
public class ChatMessageDto
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Контент.
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// Дата создания.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Отправитель.
    /// </summary>
    public SenderDto Sender { get; set; }

    /// <summary>
    /// Сообщение прочитано.
    /// </summary>
    public bool HasBeenRead { get; set; }
}

/// <summary>
/// Отправитель.
/// </summary>
public class SenderDto
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string UserName { get; set; }
}