using AutoMapper;
using MediatR;
using ZM.Application.Common.Mappings;
using ZM.Application.Dependencies.Infrastructure.Authentication;
using ZM.Application.Dependencies.Infrastructure.DataAccess.Common.Queries;
using ZM.Application.Dependencies.Infrastructure.Persistence;
using ZM.Common.Results;
using ZM.Domain.Users;

namespace ZM.Application.UseCases.Users.GetSelf;

/// <summary>
/// Запрос получение информации о себе
/// </summary>
public record GetSelfUserQuery : IRequest<Result<GetSelfUserResponse>>;

public class GetSelfUserQueryHandler(IDbContext _dbContext, ICurrentUser _currentUser, IMapper _mapper)
	: IRequestHandler<GetSelfUserQuery, Result<GetSelfUserResponse>>
{
	public async Task<Result<GetSelfUserResponse>> Handle(GetSelfUserQuery request, CancellationToken cancellationToken)
	{
		var findUser = await _dbContext.Set<User>()
			.GetByIdAsync(_currentUser.Id, cancellationToken);

		return _mapper.Map<GetSelfUserResponse>(findUser);
	}
}

public record GetSelfUserResponse : IMapFrom<User>
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
