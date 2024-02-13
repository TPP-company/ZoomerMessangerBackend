using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ZM.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services;
    }
}