using ZM.Domain.Users;
using ZM.Infrastructure.Authentication.Entities;

namespace ZM.Infrastructure.Authentication.Token;

/// <summary>
/// Сервис токена.
/// </summary>
internal interface ITokenService
{
	/// <summary>
	/// Сгенерировать.
	/// </summary>
	/// <param name="authUser">Пользователь.</param>
	/// <param name="user">Пользователь.</param>
	/// <returns>Токен.</returns>
	public TokenDto Generate(AuthUser authUser, User user);
}
