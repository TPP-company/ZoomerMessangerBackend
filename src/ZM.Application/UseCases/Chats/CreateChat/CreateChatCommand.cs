using MediatR;
using Microsoft.EntityFrameworkCore;
using ZM.Application.Common.Exceptions;
using ZM.Application.Dependencies.Infrastructure.Authentication;
using ZM.Application.Dependencies.Infrastructure.Persistence;
using ZM.Common.Results;
using ZM.Domain.Chats;
using ZM.Domain.Entities;

namespace ZM.Application.UseCases.Chats.CreateChat;

/// <summary>
/// Создать чат.
/// </summary>
/// <param name="InterlocutorId">Идентификатор собеседника.</param>
public record CreateChatCommand(Guid InterlocutorId) : IRequest<Result<ResultDataEmpty>>;

public class CreateChatCommandHandler(IDbContext _dbContext, ICurrentUser _currentUser) : IRequestHandler<CreateChatCommand, Result<ResultDataEmpty>>
{
    public async Task<Result<ResultDataEmpty>> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        var interlocutor = await _dbContext.Set<User>().SingleOrDefaultAsync(u => u.Id == request.InterlocutorId, cancellationToken)
            ?? throw new ValidationException($"Собеседник с id={request.InterlocutorId} не найден");

        var currentUser = await _dbContext.Set<User>().SingleAsync(u => u.ExternalId == _currentUser.ExternalId, cancellationToken);

        Guid[] userIds = [interlocutor.Id, currentUser.Id];

        var chatExists = await _dbContext.Set<Chat>().AnyAsync(c => c.Users.All(u => userIds.Contains(u.Id)), cancellationToken);

        if(chatExists)
            throw new ValidationException($"Чат с пользователем {interlocutor.Id} уже существует");

        var chat = new Chat(currentUser, interlocutor);

        await _dbContext.Set<Chat>().AddAsync(chat, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return ResultDataEmpty.Value;
    }
}
