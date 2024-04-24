using MediatR;
using Microsoft.EntityFrameworkCore;
using ZM.Application.Common.Exceptions;
using ZM.Application.Dependencies.Infrastructure.Persistence;
using ZM.Common.Results;
using ZM.Domain.ChatGroups;
using ZM.Domain.Users;

namespace ZM.Application.UseCases.Chats.AddMembersToChatGroup;

/// <summary>Добавить участников в чат группу.</summary>
/// <param name="Id">Идентификатор чат группы.</param>
/// <param name="MemberIds">Идентификаторы участников.</param>
public record AddMembersToChatGroupCommand(Guid Id, IReadOnlyCollection<Guid> MemberIds) : IRequest<Result<ResultDataEmpty>>;

internal class AddMembersToChatGroupCommandHandler(IDbContext _dbContext) 
	: IRequestHandler<AddMembersToChatGroupCommand, Result<ResultDataEmpty>>
{
	public async Task<Result<ResultDataEmpty>> Handle(AddMembersToChatGroupCommand request, CancellationToken cancellationToken)
	{
		var chatGroup = await _dbContext.Set<ChatGroup>()
			.Include(ch => ch.Members)
			.FirstOrDefaultAsync(ch => ch.Id == request.Id, cancellationToken) ?? throw new ResourceNotFoundException();

		var members = await _dbContext.Set<User>()
			.Where(user => request.MemberIds.Contains(user.Id))
			.ToListAsync(cancellationToken);

		chatGroup.AddMembers(members);

		return ResultDataEmpty.Value;
	}
}
