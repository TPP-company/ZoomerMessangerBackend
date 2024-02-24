using MediatR;
using Microsoft.EntityFrameworkCore;
using ZM.Application.Common.Exceptions;
using ZM.Application.Dependencies.Infrastructure.Authentication;
using ZM.Application.Dependencies.Infrastructure.Persistence;
using ZM.Common.Results;
using ZM.Domain.Chats;
using ZM.Domain.Users;

namespace ZM.Application.UseCases.Chats.CreateP2PChat;

/// <summary>
/// Создать чат.
/// </summary>
/// <param name="InterlocutorId">Идентификатор собеседника.</param>
public record CreateP2PChatCommand(Guid InterlocutorId) : IRequest<Result<ResultDataEmpty>>;

public class CreateP2PChatCommandHandler(IDbContext _dbContext, ICurrentUser _currentUser) : IRequestHandler<CreateP2PChatCommand, Result<ResultDataEmpty>>
{
	public async Task<Result<ResultDataEmpty>> Handle(CreateP2PChatCommand request, CancellationToken cancellationToken)
	{
		var interlocutor = await _dbContext.Set<User>().SingleOrDefaultAsync(u => u.Id == request.InterlocutorId, cancellationToken)
			?? throw new ValidationException($"Собеседник с id={request.InterlocutorId} не найден");

		var currentUser = await _dbContext.Set<User>().SingleAsync(u => u.ExternalId == _currentUser.ExternalId, cancellationToken);

		Guid[] userIds = [interlocutor.Id, currentUser.Id];

		var chatExists = await _dbContext.Set<P2PChat>().AnyAsync(c => c.Users.All(u => userIds.Contains(u.Id)), cancellationToken);

		if (chatExists)
			throw new ValidationException($"Чат с пользователем {interlocutor.Id} уже существует");

		var chat = new P2PChat(currentUser, interlocutor);

		await _dbContext.Set<P2PChat>().AddAsync(chat, cancellationToken);
		await _dbContext.SaveChangesAsync(cancellationToken);

		return ResultDataEmpty.Value;
	}
}
