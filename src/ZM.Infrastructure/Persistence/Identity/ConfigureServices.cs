using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ZM.Infrastructure.Persistence.Identity;
internal static class ConfigureServices
{
    public static void AddIdentityPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AuthDbContext>(opt =>
        {
            opt.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                opt =>
                {
                    opt.MigrationsHistoryTable(HistoryRepository.DefaultTableName, AuthDbContext.DefaultSchema);
                });
        });
    }
}
