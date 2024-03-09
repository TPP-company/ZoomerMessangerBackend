using MediatR;
using Microsoft.EntityFrameworkCore;
using ZM.Application.Dependencies.Infrastructure.Authentication;
using ZM.Application.Dependencies.Infrastructure.Persistence;
using ZM.Common.Results;
using ZM.Domain.ChatGroups;
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
		var p2pChats = await _dbContext.Set<P2PChat>()
			.Where(chat => chat.Members.Any(u => u.Id == _currentUser.Id))
			.Select(chat => new GetChatsDto()
			{
				Id = chat.Id,
				Title = chat.Members.First(u => u.Id != _currentUser.Id).UserName,
				InterlocutorId = chat.Members.First(u => u.Id != _currentUser.Id).Id
			})
			.ToListAsync(cancellationToken);

		var chatGroups = await _dbContext.Set<ChatGroup>()
			.Where(chat => chat.Members.Any(u => u.Id == _currentUser.Id))
			.Select(chat => new GetChatsDto()
			{
				Id = chat.Id,
				Title = chat.Name,
			})
			.ToListAsync(cancellationToken);

		return p2pChats.Concat(chatGroups).ToList().AsReadOnly();
	}
}

public class GetChatsDto
{
	/// <summary>
	/// Идентификатор.
	/// </summary>
	public Guid Id { get; set; }

	/// <summary>
	/// Название.
	/// </summary>
	public string Title { get; set; } = null!;

	/// <summary>
	/// Идентификатор собеседника.
	/// </summary>
	public Guid? InterlocutorId { get; set; }
}
