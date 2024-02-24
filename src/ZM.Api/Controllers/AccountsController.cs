using Microsoft.AspNetCore.Mvc;
using ZM.Infrastructure.Authentication.Services;

namespace ZM.Api.Controllers;

/// <summary>
/// Апи аккаунта.
/// </summary>
[ApiController]
[Route("accounts")]
public class AccountsController(IAuthenticationService _authenticationService) : ApiControllerBase
{
	/// <summary>
	/// Зарегистрироваться.
	/// </summary>
	/// <param name="request">Информация для регистрации.</param>
	[HttpPost]
	public async Task<ActionResult> SignUpAsync(SignUpRequest request)
	{
		var result = await _authenticationService.SignUpAsync(request);

		return result.IsFailure ? BadRequest(result.Error) : Ok();
	}
}
