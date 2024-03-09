using Microsoft.AspNetCore.Mvc;
using ZM.Common.Results;
using ZM.Infrastructure.Authentication.Services;
using ZM.Infrastructure.Authentication.Token;

namespace ZM.Api.Controllers;

/// <summary>
/// Апи сессий.
/// </summary>
[ApiController]
[Route("sessions")]
public class SessionsController(IAuthenticationService _authenticationService) : ApiControllerBase
{
	/// <summary>
	/// Выполнить вход.
	/// </summary>
	/// <param name="request">Информация для входа.</param>
	[HttpPost()]
	public async Task<ActionResult<Result<TokenDto>>> SignInAsync(SignInRequest request)
	{
		var result = await _authenticationService.SignInAsync(request);

		return result.IsFailure ? BadRequest(result.Error) : Ok(result.Data);
	}

	//TODO: Refresh token
}
