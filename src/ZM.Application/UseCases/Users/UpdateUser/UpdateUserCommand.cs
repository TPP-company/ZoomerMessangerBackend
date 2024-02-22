using MediatR;
using Microsoft.EntityFrameworkCore;
using ZM.Application.Dependencies.Infrastructure.Authentication;
using ZM.Application.Dependencies.Infrastructure.Persistence;
using ZM.Common.Results;
using ZM.Domain.Entities;

namespace ZM.Application.UseCases.Users.UpdateUser;

public record UpdateUserCommand(string About, Guid AvatarId) : IRequest<Result<ResultDataEmpty>>;

public class UpdateUserCommandHandler(IDbContext _dbContext, ICurrentUser currentUser) : IRequestHandler<UpdateUserCommand, Result<ResultDataEmpty>>
{   
    /// <summary>
    /// Хендлер для обновление данных юзера 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Result<ResultDataEmpty>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var findUser = await _dbContext.Set<User>().FirstAsync(x => x.ExternalId == currentUser.ExternalId, cancellationToken);
        findUser.About = request.About;
        findUser.AvatarId = request.AvatarId;
        _ = await _dbContext.SaveChangesAsync();

        return ResultDataEmpty.Value;
    }
}


