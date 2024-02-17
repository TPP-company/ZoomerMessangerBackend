using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZM.Application.Dependencies.Infrastructure.Persistence;
using ZM.Application.UseCases.Chats.CreateChat;
using ZM.Application.UseCases.Chats.GetChatMessages;
using ZM.Application.UseCases.Chats.GetChats;
using ZM.Common.Results;

namespace ZM.Api.Controllers;

/// <summary>
/// Апи чатов.
/// </summary>
[Authorize]
[ApiController]
[Route("chats")]
public class ChatsController(ISender _sender, IDbContext _dbContext) : ApiControllerBase
{
    /// <summary>
    /// Получить все свои чаты.
    /// </summary>
    [HttpGet]
    public Task<Result<IReadOnlyCollection<GetChatsDto>>> GetAsync()
        => _sender.Send(new GetChatsQuery());

    /// <summary>
    /// Получить сообщения чата.
    /// </summary>
    [HttpGet("{id}/messages")]
    public Task<Result<IReadOnlyCollection<ChatMessageDto>>> GetChatMessagesAsync(Guid id)
        => _sender.Send(new GetChatMessagesQuery(id));

    /// <summary>
    /// Создать.
    /// </summary>
    [HttpPost]
    public Task<Result<ResultDataEmpty>> CreateAsync(Guid interlocutorId)
        => _sender.Send(new CreateChatCommand(interlocutorId));
}
