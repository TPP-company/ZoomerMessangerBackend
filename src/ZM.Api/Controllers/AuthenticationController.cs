﻿using Microsoft.AspNetCore.Mvc;
using ZM.Infrastructure.Authentication.Services;
using ZM.Application.Common.Results;
using ZM.Infrastructure.Authentication.Token;

namespace ZM.Api.Controllers;

/// <summary>
/// Апи аутентификации.
/// </summary>
[ApiController]
[Route("authentication")]
public class AuthenticationController(IAuthenticationService _authenticationService) : ApiControllerBase
{
    /// <summary>
    /// Выполнить вход.
    /// </summary>
    /// <param name="request">Информация для входа.</param>
    [HttpPost("sign-in")]
    public async Task<ActionResult<Result<TokenDto>>> SignInAsync(SignInRequest request)
    {
        var result = await _authenticationService.SignIn(request);

        return result.IsFailure ? 
            result.Error!.SignInErrorType switch
            {
                SignInErrorType.Invalid => BadRequest(result.Error),
                _ => BadRequest()
            } :
            Result<TokenDto>.Success(result.Data!);
    }

    /// <summary>
    /// Зарегистрироваться.
    /// </summary>
    /// <param name="request">Информация для регистрации.</param>
    [HttpPost("sign-up")]
    public async Task<ActionResult<Result<ResultDataEmpty>>> SignUpAsync(SignUpRequest request)
    {
        var result = await _authenticationService.SignUp(request);

        return result.IsFailure ? BadRequest(result.Error) : Ok();
    }
}