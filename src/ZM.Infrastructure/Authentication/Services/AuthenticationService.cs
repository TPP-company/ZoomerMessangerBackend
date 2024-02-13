using Microsoft.AspNetCore.Identity;
using ZM.Application.Common.Results;
using ZM.Infrastructure.Authentication.Entities;
using ZM.Infrastructure.Authentication.Token;

namespace ZM.Infrastructure.Authentication.Services;

/// <summary>
/// <inheritdoc cref="IAuthenticationService"/>
/// </summary>
public class AuthenticationService(UserManager<AuthUser> _userManager, IJwtTokenService _jwtTokenService) 
    : IAuthenticationService
{
    public async Task<Result<TokenDto, SignInError>> SignIn(SignInRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);

        var isSignInInvalid = user is null || !await _userManager.CheckPasswordAsync(user, request.Password);

        if (isSignInInvalid)
            return Result<TokenDto, SignInError>.Fail(new SignInError("Ошибочка") 
            { 
                SignInErrorType = SignInErrorType.Invalid 
            });

        return _jwtTokenService.Generate(user!);
    }

    public async Task<Result<ResultDataEmpty>> SignUp(SignUpRequest request)
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

public class SignInError : Error
{
    public SignInError()
    {
        
    }

    public SignInError(string code) : base(code)
    {
    }

    public SignInError(string code, string? description) : base(code, description)
    {
    }

    public SignInError(string code, Dictionary<string, string[]> reason) : base(code, reason)
    {
    }

    public SignInError(string code, string? description, Dictionary<string, string[]> reason) : base(code, description, reason)
    {
    }

    public SignInErrorType SignInErrorType { get; set; }
}

public enum SignInErrorType
{
    None = 0,
    Invalid
}