using MediatR;
using Microsoft.EntityFrameworkCore;
using ZM.Application.Dependencies.Infrastructure.Authentication;
using ZM.Application.Dependencies.Infrastructure.Persistence;
using ZM.Common.Results;
using ZM.Domain.Entities;

namespace ZM.Application.UseCases.Users.UpdateUser;

/// <summary>
/// Для обновление данных пользователя
/// </summary>
/// <param name="About">О себе</param>
/// <param name="AvatarId">Аватарка</param>
public record UpdateUserCommand(string About, Guid AvatarId) : IRequest<Result<ResultDataEmpty>>;

public class UpdateUserCommandHandler(IDbContext _dbContext, ICurrentUser currentUser) : IRequestHandler<UpdateUserCommand, Result<ResultDataEmpty>>
{
    public async Task<Result<ResultDataEmpty>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var findUser = await _dbContext.Set<User>().FirstAsync(x => x.ExternalId == currentUser.ExternalId, cancellationToken);
        findUser.About = request.About;
        findUser.AvatarId = request.AvatarId;
        _ = await _dbContext.SaveChangesAsync(cancellationToken);

        return ResultDataEmpty.Value;
    }
}
