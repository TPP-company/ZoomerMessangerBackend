using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZM.Application.UseCases.Users.GetSelf;
using ZM.Application.UseCases.Users.UpdateSelf;
using ZM.Common.Results;

namespace ZM.Api.Controllers;

/// <summary>
/// Апи пользователей.
/// </summary>
[ApiController]
[Route("users")]
[Authorize]
public class UsersController : ApiControllerBase
{
	/// <summary>
	/// Обновить информацию о себе
	/// </summary>
	[HttpPut("self")]
	public Task<Result<ResultDataEmpty>> UpdateSelfAsync([FromBody] UpdateSelfUserCommand request)
		=> Sender.Send(request);

	/// <summary>
	/// Получение информации о текущем пользователе
	/// </summary>
	[HttpGet("self")]
	public Task<Result<GetSelfUserResponse>> GetSelfAsync()
		=> Sender.Send(new GetSelfUserQuery());
}
