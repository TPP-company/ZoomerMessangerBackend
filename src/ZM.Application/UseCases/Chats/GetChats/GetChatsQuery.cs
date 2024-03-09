using MediatR;
using Microsoft.EntityFrameworkCore;
using ZM.Application.Dependencies.Infrastructure.Authentication;
using ZM.Application.Dependencies.Infrastructure.Persistence;
using ZM.Common.Results;
using ZM.Domain.Chats;

namespace ZM.Application.UseCases.Chats.GetChats;

/// <summary>
/// Получить чаты.
/// </summary>
public record GetChatsQuery : IRequest<Result<IReadOnlyCollection<GetChatsDto>>>;

public class GetChatsQueryHandler(IDbContext _dbContext, ICurrentUser _currentUser)
	: IRequestHandler<GetChatsQuery, Result<IReadOnlyCollection<GetChatsDto>>>
{
	public async Task<Result<IReadOnlyCollection<GetChatsDto>>> Handle(GetChatsQuery request, CancellationToken cancellationToken)
	{
		var dtos = await _dbContext.Set<P2PChat>()
			.Where(chat => chat.Users.Any(u => u.Id == _currentUser.Id))
			.Select(chat => new GetChatsDto()
			{
				Id = chat.Id,
				Interlocutor = new()
				{
					Id = chat.Users.First(u => u.Id != _currentUser.Id).Id,
					UserName = chat.Users.First(u => u.Id != _currentUser.Id).UserName,
				}
			})
			.ToListAsync(cancellationToken);

		return dtos;
	}
}

public class GetChatsDto
{
	/// <summary>
	/// Идентификатор.
	/// </summary>
	public Guid Id { get; set; }

	/// <summary>
	/// Собеседник.
	/// </summary>
	public InterlocutorDto Interlocutor { get; set; } = null!;
}

/// <summary>
/// Собеседник.
/// </summary>
public class InterlocutorDto
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