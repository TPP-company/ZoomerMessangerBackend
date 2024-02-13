using ZM.Infrastructure.Authentication.Entities;

namespace ZM.Infrastructure.Authentication.Token;

/// <summary>
/// Сервис JWT токена.
/// </summary>
public interface IJwtTokenService
{
    /// <summary>
    /// Сгенерировать.
    /// </summary>
    /// <param name="authUser">Пользователь.</param>
    /// <returns>Токен.</returns>
    public TokenDto Generate(AuthUser authUser);
}
