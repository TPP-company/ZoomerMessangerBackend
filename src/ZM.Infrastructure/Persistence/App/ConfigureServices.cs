using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZM.Application.Dependencies.Infrastructure.Persistence;

namespace ZM.Infrastructure.Persistence.App;
internal static class ConfigureServices
{
    public static void AddAppPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IDbContext, AppDbContext>();
    }
}
