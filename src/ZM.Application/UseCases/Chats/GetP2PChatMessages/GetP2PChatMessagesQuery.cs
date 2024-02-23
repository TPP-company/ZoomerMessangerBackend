using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZM.Application.Common.Exceptions;
using ZM.Application.Common.Mappings;
using ZM.Application.Common.Models.Pagination;
using ZM.Application.Dependencies.Infrastructure.Authentication;
using ZM.Application.Dependencies.Infrastructure.DataAccess.Common.Queries;
using ZM.Application.Dependencies.Infrastructure.Persistence;
using ZM.Domain.Chats;
using ZM.Domain.Entities;

namespace ZM.Application.UseCases.Chats.GetP2PChatMessages;

/// <summary>
/// Получить сообщения чата.
/// </summary>
/// <param name="ChatId">Идентификатор чата.</param>
public record GetP2PChatMessagesQuery(Guid ChatId) : PagedAndSorted, IRequest<PaginatedResponse<P2PChatMessageDto>>;

public class GetP2PChatMessagesQueryHandler(IDbContext _dbContext, ICurrentUser _currentUser, IMapper _mapper)
	: IRequestHandler<GetP2PChatMessagesQuery, PaginatedResponse<P2PChatMessageDto>>
{
	public async Task<PaginatedResponse<P2PChatMessageDto>> Handle(GetP2PChatMessagesQuery request, CancellationToken cancellationToken)
	{
		var chatExists = await _dbContext.Set<P2PChat>()
			.SingleOrDefaultAsync(
				ch => ch.Id == request.ChatId && ch.Users.Any(u => u.ExternalId == _currentUser.ExternalId),
				cancellationToken)
			?? throw new ResourceNotFoundException();

		var chatMessages = await _dbContext.Set<P2PChatMessage>()
			.Where(m => m.ChatId == request.ChatId)
			.OrderBy(m => m.CreatedDate)
			.GetPaginatedResponseAsync<P2PChatMessage, P2PChatMessageDto>(request, null, _mapper, cancellationToken);

		return chatMessages;
	}
}

/// <summary>
/// Сообщение чата.
/// </summary>
public class P2PChatMessageDto : IMapFrom<P2PChatMessage>
{
	/// <summary>
	/// Идентификатор.
	/// </summary>
	public Guid Id { get; set; }

	/// <summary>
	/// Контент.
	/// </summary>
	public string Content { get; set; }

	/// <summary>
	/// Дата создания.
	/// </summary>
	public DateTime CreatedDate { get; set; }

	/// <summary>
	/// Отправитель.
	/// </summary>
	public SenderDto Sender { get; set; }

	/// <summary>
	/// Сообщение прочитано.
	/// </summary>
	public bool HasBeenRead { get; set; }
}

/// <summary>
/// Отправитель.
/// </summary>
public class SenderDto : IMapFrom<User>
{
	/// <summary>
	/// Идентификатор.
	/// </summary>
	public Guid Id { get; set; }

	/// <summary>
	/// Имя пользователя.
	/// </summary>
	public string UserName { get; set; }
}