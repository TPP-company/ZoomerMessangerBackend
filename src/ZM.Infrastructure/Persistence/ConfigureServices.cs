using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZM.Infrastructure.Persistence.App;
using ZM.Infrastructure.Persistence.Identity;

namespace ZM.Infrastructure.Persistence;
public static class ConfigureServices
{
	public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddAppPersistence(configuration);
		services.AddIdentityPersistence(configuration);
	}
}
