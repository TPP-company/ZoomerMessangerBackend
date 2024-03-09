using MediatR;
using ZM.Application.Dependencies.Infrastructure.Authentication;
using ZM.Application.Dependencies.Infrastructure.DataAccess.Common.Queries;
using ZM.Application.Dependencies.Infrastructure.Persistence;
using ZM.Common.Results;
using ZM.Domain.Users;

namespace ZM.Application.UseCases.Users.UpdateSelf;

/// <summary>
/// Команда обновления данных о себе
/// </summary>
/// <param name="About">О себе</param>
/// <param name="AvatarId">Аватарка</param>
public record UpdateSelfUserCommand(string About, Guid AvatarId) : IRequest<Result<ResultDataEmpty>>;

public class UpdateSelfUserCommandHandler(IDbContext _dbContext, ICurrentUser _currentUser)
	: IRequestHandler<UpdateSelfUserCommand, Result<ResultDataEmpty>>
{
	public async Task<Result<ResultDataEmpty>> Handle(UpdateSelfUserCommand request, CancellationToken cancellationToken)
	{
		var findUser = await _dbContext.Set<User>().GetByIdAsync(_currentUser.Id, cancellationToken);
		findUser!.About = request.About;
		findUser.AvatarId = request.AvatarId;

		await _dbContext.SaveChangesAsync(cancellationToken);

		return ResultDataEmpty.Value;
	}
}
