using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZM.Application.UseCases.Chats.CreateChatGroup;
using ZM.Common.Results;

namespace ZM.Api.Controllers;

/// <summary>
/// Апи групп чатов.
/// </summary>
[Authorize]
[ApiController]
[Route("groups")]
public class ChatGroupsController : ChatsController
{
    /// <summary>
    /// Создать.
    /// </summary>
    [HttpPost]
    public Task<Result<ResultDataEmpty>> CreateAsync(CreateChatGroupCommand request)
        => Sender.Send(request);
}
