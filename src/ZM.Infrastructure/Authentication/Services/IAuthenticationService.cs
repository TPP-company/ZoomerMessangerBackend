using ZM.Application.Common.Results;
using ZM.Infrastructure.Authentication.Token;

namespace ZM.Infrastructure.Authentication.Services;

/// <summary>
/// Сервис аутентификации.
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// Выполнить вход.
    /// </summary>
    /// <param name="request">Информация для входа.</param>
    Task<Result<TokenDto>> SignInAsync(SignInRequest request);

    /// <summary>
    /// Зарегистрироваться.
    /// </summary>
    /// <param name="request">Информация для регистрации.</param>
    Task<Result<ResultDataEmpty>> SignUpAsync(SignUpRequest request);
}
