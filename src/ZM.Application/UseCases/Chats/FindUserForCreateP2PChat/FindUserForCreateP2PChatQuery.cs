using AutoMapper;
using MediatR;
using ZM.Application.Common.Extensions;
using ZM.Application.Common.Mappings;
using ZM.Application.Common.Models.Pagination;
using ZM.Application.Dependencies.Infrastructure.Authentication;
using ZM.Application.Dependencies.Infrastructure.DataAccess.Common.Queries;
using ZM.Application.Dependencies.Infrastructure.Persistence;
using ZM.Domain.Users;

namespace ZM.Application.UseCases.Chats.FindUserForCreateP2PChat;

/// <summary>
/// Найти пользователей.
/// </summary>
public record FindUserForCreateP2PChatQuery(string? UserName) : Paged, IRequest<PaginatedResponse<FindUserForCreateP2PChatResponse>>;

public class FindUserForCreateP2PChatQueryHandler(IDbContext _dbContext, IMapper _mapper, ICurrentUser _currentUser)
	: IRequestHandler<FindUserForCreateP2PChatQuery, PaginatedResponse<FindUserForCreateP2PChatResponse>>
{
	public async Task<PaginatedResponse<FindUserForCreateP2PChatResponse>> Handle(FindUserForCreateP2PChatQuery request, CancellationToken cancellationToken)
	{
		return await _dbContext.Set<User>()
			.WhereIf(!string.IsNullOrWhiteSpace(request.UserName), u => u.UserName.StartsWith(request.UserName!))
			.Where(u => !u.P2PChats.Any(p2p => p2p.Users.Any(u => u.ExternalId == _currentUser.ExternalId)))
			.GetPaginatedResponseAsync<User, FindUserForCreateP2PChatResponse>(
				request,
				_mapper,
				cancellationToken);
	}
}

/// <summary>
/// Ответ на запрос поиска пользователя для создания чата P2P.
/// </summary>
public class FindUserForCreateP2PChatResponse : IMapFrom<User>
{
	/// <summary>
	/// Идентификатор.
	/// </summary>
	public Guid Id { get; set; }

	/// <summary>
	/// Имя пользователя.
	/// </summary>
	public string UserName { get; set; } = null!;
}
