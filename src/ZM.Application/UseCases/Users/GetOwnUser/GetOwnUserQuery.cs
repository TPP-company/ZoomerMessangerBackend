using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZM.Application.Common.Mappings;
using ZM.Application.Dependencies.Infrastructure.Authentication;
using ZM.Application.Dependencies.Infrastructure.Persistence;
using ZM.Common.Results;
using ZM.Domain.Entities;

namespace ZM.Application.UseCases.Users.GetOwnUser;

/// <summary>
/// Запрос получение для текущего пользователя 
/// </summary>
public record GetOwnUserQuery : IRequest<Result<GetOwnUserResponse>>;
public class GetOwnUserQueryHandler(IDbContext _dbContext, ICurrentUser currentUser, IMapper mapper) : IRequestHandler<GetOwnUserQuery, Result<GetOwnUserResponse>>
{

	public async Task<Result<GetOwnUserResponse>> Handle(GetOwnUserQuery request, CancellationToken cancellationToken)
	{
		var findUser = await _dbContext.Set<User>().FirstAsync(user => user.ExternalId == currentUser.ExternalId);

		return mapper.Map<GetOwnUserResponse>(findUser);
	}
}


public record GetOwnUserResponse : IMapFrom<User>
{

	public Guid Id { get; init; }
	public string UserName { get; init; } = null!;

	/// <summary>
	/// Информация о.
	/// </summary>
	public string? About { get; init; }

	/// <summary>
	/// Идентификатор аватарки.
	/// </summary>
	public Guid? AvatarId { get; init; }

	/// <summary>
	/// Дата регистрации.
	/// </summary>
	public DateTime DateOfRegistration { get; init; }

	/// <summary>
	/// Идентификатор внешнего пользователя.
	/// </summary>
	public Guid ExternalId { get; init; }

}