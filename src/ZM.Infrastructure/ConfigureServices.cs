using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZM.Infrastructure.Authentication;
using ZM.Infrastructure.Authorization;
using ZM.Infrastructure.Persistence;
using ZM.Infrastructure.Swagger;

namespace ZM.Infrastructure;

public static class ConfigureServices
{
    /// <summary>
    /// Добавить инфраструктуру.
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.ConfigureAuthentication(configuration);
        services.ConfigureAuthorization(configuration);
        services.ConfigureSwagger(configuration);

        return services;
    }
}