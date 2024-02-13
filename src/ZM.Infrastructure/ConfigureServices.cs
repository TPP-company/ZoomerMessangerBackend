using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZM.Infrastructure.Authentication;
using ZM.Infrastructure.Persistence;

namespace ZM.Infrastructure;

public static class ConfigureServices
{
    /// <summary>
    /// Добавить инфраструктуру.
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddAuth(configuration);

        return services;
    }
}