using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZM.Application.UseCases.Chats.GetChats;
using ZM.Common.Results;
using ZM.Infrastructure.RoutePrefix;

namespace ZM.Api.Controllers;

/// <summary>
/// Апи чатов.
/// </summary>
[Authorize]
[ApiController]
[RoutePrefix("chats")]
[Route("")]
public class ChatsController : ApiControllerBase
{
	/// <summary>
	/// Получить все свои чаты.
	/// </summary>
	[HttpGet]
	public Task<Result<IReadOnlyCollection<GetChatsDto>>> GetAsync()
		=> Sender.Send(new GetChatsQuery());
}
