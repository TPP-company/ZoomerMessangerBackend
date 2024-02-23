using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ZM.Infrastructure.Authorization;
public static class ConfigureServices
{
	/// <summary>
	/// Добавить авторизацию.
	/// </summary>
	public static void ConfigureAuthorization(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddAuthorization();
	}
}
