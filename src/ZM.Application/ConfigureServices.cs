using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ZM.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
        });

        services.AddAutoMapper(assembly);

        return services;
    }
}