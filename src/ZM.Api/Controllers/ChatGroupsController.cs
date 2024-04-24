using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZM.Application.UseCases.Chats.AddMembersToChatGroup;
using ZM.Application.UseCases.Chats.CreateChatGroup;
using ZM.Common.Results;

namespace ZM.Api.Controllers;

/// <summary>Апи групп чатов.</summary>
[Authorize]
[ApiController]
[Route("groups")]
public class ChatGroupsController : ChatsController
{
	/// <summary>Создать.</summary>
	[HttpPost]
	public Task<Result<ResultDataEmpty>> CreateAsync(CreateChatGroupCommand request)
		=> Sender.Send(request);

	//TODO: передавать класс в котором будет название группового чата. Обновляем только название.
	/// <summary>Обновить.</summary>
	[HttpPut]
	public Task<Result<ResultDataEmpty>> UpdateAsync()
		=> throw new NotImplementedException();

	//TODO: получаем участников без пагинации и фильтров
	/// <summary>Получить участников.</summary>
	/// <param name="id">Идентификатор группы.</param>
	[HttpGet("{id:guid}/members")]
	public Task<Result<ResultDataEmpty>> GetMembersAsync(Guid id)
		=> throw new NotImplementedException();

	/// <summary>Добавить участников.</summary>
	/// <param name="id">Идентификатор группы.</param>
	/// <param name="memberIds">Идентификаторы участников.</param>
	[HttpPost("{id:guid}/members")]
	public Task<Result<ResultDataEmpty>> AddMembersAsync(Guid id, IReadOnlyCollection<Guid> memberIds)
		=> Sender.Send(new AddMembersToChatGroupCommand(id, memberIds));

	//TODO: удаляем участника.
	/// <summary>Удалить участника.</summary>
	/// <param name="id">Идентификатор группы.</param>
	/// <param name="memberId">Идентификатор участника.</param>
	[HttpDelete("{id:guid}/members/{memberId:guid}")]
	public Task<Result<ResultDataEmpty>> RemoveMemberAsync(Guid id, Guid memberId)
		=> throw new NotImplementedException();
}
