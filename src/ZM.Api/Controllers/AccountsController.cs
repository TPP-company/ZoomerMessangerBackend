using Microsoft.AspNetCore.Mvc;
using ZM.Infrastructure.Authentication.Services;
using ZM.Infrastructure.Authentication.Token;
using ZM.Common.Results;

namespace ZM.Api.Controllers;

/// <summary>
/// Апи аккаунта.
/// </summary>
[ApiController]
[Route("accounts")]
public class AccountsController(IAuthenticationService _authenticationService) : ApiControllerBase
{
    /// <summary>
    /// Выполнить вход.
    /// </summary>
    /// <param name="request">Информация для входа.</param>
    [HttpPost("sign-in")]
    public async Task<ActionResult<Result<TokenDto>>> SignInAsync(SignInRequest request)
    {
        var result = await _authenticationService.SignInAsync(request);

        return result.IsFailure ? BadRequest(result.Error) : Ok(result.Data);
    }

    /// <summary>
    /// Зарегистрироваться.
    /// </summary>
    /// <param name="request">Информация для регистрации.</param>
    [HttpPost("sign-up")]
    public async Task<ActionResult<Result<ResultDataEmpty>>> SignUpAsync(SignUpRequest request)
    {
        var result = await _authenticationService.SignUpAsync(request);

        return result.IsFailure ? BadRequest(result.Error) : Ok();
    }
}
