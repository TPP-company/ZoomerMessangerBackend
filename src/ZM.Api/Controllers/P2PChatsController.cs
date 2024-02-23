using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZM.Api.Requests;
using ZM.Application.Common.Models.Pagination;
using ZM.Application.UseCases.Chats.CreateP2PChat;
using ZM.Application.UseCases.Chats.GetP2PChatMessages;
using ZM.Common.Results;

namespace ZM.Api.Controllers;

/// <summary>
/// Апи чатов.
/// </summary>
[Authorize]
[ApiController]
[Route("p2p")]
public class P2PChatsController : ChatsController
{
	/// <summary>
	/// Получить сообщения чата.
	/// </summary>
	[HttpGet("{id}/messages")]
	public Task<PaginatedResponse<P2PChatMessageDto>> GetChatMessagesAsync(Guid id, [FromQuery] GetChatMessagesRequest request)
		=> Sender.Send(new GetP2PChatMessagesQuery(id)
		{
			PageNumber = request.PageNumber,
			PageSize = request.PageSize,
			Sorting = request.Sorting,
		});

	/// <summary>
	/// Создать.
	/// </summary>
	[HttpPost]
	public Task<Result<ResultDataEmpty>> CreateAsync(Guid interlocutorId)
		=> Sender.Send(new CreateP2PChatCommand(interlocutorId));
}
