using MediatR;
using Microsoft.EntityFrameworkCore;
using ZM.Application.Dependencies.Infrastructure.Authentication;
using ZM.Application.Dependencies.Infrastructure.DataAccess.Common.Queries;
using ZM.Application.Dependencies.Infrastructure.Persistence;
using ZM.Common.Results;
using ZM.Domain.ChatGroups;
using ZM.Domain.Users;

namespace ZM.Application.UseCases.Chats.CreateChatGroup;

/// <summary>
/// Команда создания чат группы.
/// </summary>
public record CreateChatGroupCommand(string Name, IReadOnlyCollection<Guid>? MembersIds) : IRequest<Result<ResultDataEmpty>>;

public class CreateChatGroupCommandHandler(IDbContext _dbContext, ICurrentUser _currentUser) 
    : IRequestHandler<CreateChatGroupCommand, Result<ResultDataEmpty>>
{
    public async Task<Result<ResultDataEmpty>> Handle(CreateChatGroupCommand request, CancellationToken cancellationToken)
    {
        var currentUser = await _dbContext.Set<User>().GetByIdAsync(_currentUser.Id, cancellationToken);

        List<User> members = [currentUser];

		if (request.MembersIds is not null && request.MembersIds.Count != 0)
        {
            var requestMembers = await _dbContext.Set<User>()
                .Where(u => request.MembersIds.Contains(u.Id))
                .ToListAsync(cancellationToken);

            members.AddRange(requestMembers);
        }

        var chatGroup = new ChatGroup(request.Name, currentUser!.Id, members);
		await _dbContext.Set<ChatGroup>().AddAsync(chatGroup, cancellationToken);

		await _dbContext.SaveChangesAsync(cancellationToken);

		return ResultDataEmpty.Value;
    }
}
