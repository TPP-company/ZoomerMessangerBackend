using Microsoft.AspNetCore.Identity;
using System.Transactions;
using ZM.Application.Dependencies.Infrastructure.Persistence;
using ZM.Common.Results;
using ZM.Domain.Entities;
using ZM.Infrastructure.Authentication.Entities;
using ZM.Infrastructure.Authentication.Token;

namespace ZM.Infrastructure.Authentication.Services;

/// <summary>
/// <inheritdoc cref="IAuthenticationService"/>
/// </summary>
internal class AuthenticationService(
    UserManager<AuthUser> _userManager,
    ITokenService _jwtTokenService,
    IDbContext _dbContext,
    TimeProvider _timeProvider) : IAuthenticationService
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
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var authUser = new AuthUser()
        {
            UserName = request.Username
        };

        try
        {
            var createUserResult = await _userManager.CreateAsync(authUser, request.Password);

            if (!createUserResult.Succeeded)
                return Result<ResultDataEmpty>.Fail("Authentication.SignUp");

            var user = new User()
            {
                ExternalId = authUser.Id,
                UserName = request.Username,
                DateOfRegistration = _timeProvider.GetUtcNow().UtcDateTime,
            };

            _dbContext.Set<User>().Add(user);
            await _dbContext.SaveChangesAsync();

            scope.Complete();
        }
        catch
        {
            scope.Dispose();
            throw;
        }

        return ResultDataEmpty.Value;
    }
}
