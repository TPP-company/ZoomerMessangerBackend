using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZM.Application.UseCases.Chats.CreateChat;
using ZM.Application.UseCases.Chats.GetChats;
using ZM.Common.Results;

namespace ZM.Api.Controllers;

/// <summary>
/// Апи чатов.
/// </summary>
[Authorize]
[ApiController]
[Route("chats")]
public class ChatsController(ISender _sender) : ApiControllerBase
{
    /// <summary>
    /// Получить все свои чаты.
    /// </summary>
    [HttpGet]
    public Task<Result<IReadOnlyCollection<GetChatsDto>>> GetAsync()
        => _sender.Send(new GetChatsQuery());

    /// <summary>
    /// Создать.
    /// </summary>
    [HttpPost]
    public Task<Result<ResultDataEmpty>> CreateAsync(Guid interlocutorId)
        => _sender.Send(new CreateChatCommand(interlocutorId));
}
