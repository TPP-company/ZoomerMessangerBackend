using Microsoft.AspNetCore.Identity;
using ZM.Application.Common.Results;
using ZM.Infrastructure.Authentication.Entities;
using ZM.Infrastructure.Authentication.Token;

namespace ZM.Infrastructure.Authentication.Services;

/// <summary>
/// <inheritdoc cref="IAuthenticationService"/>
/// </summary>
public class AuthenticationService(
    UserManager<AuthUser> _userManager,
    ITokenService _jwtTokenService) 
    : IAuthenticationService
{
    public async Task<Result<TokenDto>> SignInAsync(SignInRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);

        var isSignInInvalid = user is null || !await _userManager.CheckPasswordAsync(user, request.Password);

        if (isSignInInvalid)
            return Result<TokenDto>.Fail("Authentication.SignIn");

        return _jwtTokenService.Generate(user!);
    }

    public async Task<Result<ResultDataEmpty>> SignUpAsync(SignUpRequest request)
    {
        var user = new AuthUser()
        {
            UserName = request.Username
        };

        var createUserResult = await _userManager.CreateAsync(user, request.Password);

        if (!createUserResult.Succeeded)
            return Result<ResultDataEmpty>.Fail("Authentication.SignUp");

        return ResultDataEmpty.Value;
    }
}
