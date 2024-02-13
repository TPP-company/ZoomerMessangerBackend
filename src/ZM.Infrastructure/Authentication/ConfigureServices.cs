using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZM.Application.Dependencies.Infrastructure.Authentication;
using ZM.Infrastructure.Authentication.Entities;
using ZM.Infrastructure.Authentication.Services;
using ZM.Infrastructure.Authentication.Token;
using ZM.Infrastructure.Persistence;

namespace ZM.Infrastructure.Authentication;
public static class ConfigureServices
{
    /// <summary>
    /// Добавить аутентификацию.
    /// </summary>
    public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<AuthUser, AuthRole>(opt => { })
            .AddEntityFrameworkStores<AppDbContext>();

        services.AddScoped<IAuthenticationService, AuthenticationService>();    
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
    }
}
