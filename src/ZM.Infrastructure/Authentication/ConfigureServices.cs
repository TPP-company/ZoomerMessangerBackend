using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
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
    public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<AuthUser, AuthRole>(opt => { })
            .AddEntityFrameworkStores<AppDbContext>();

        services
            .AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new()
                {
                    ValidIssuer = "zm-backend",
                    ValidAudience = "zm-flatter",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("supersecret123123adminmazerratty!!!")),
                };
            });

        services.AddScoped<IAuthenticationService, AuthenticationService>();    
        services.AddScoped<ITokenService, JwtTokenService>();
        services.AddScoped<ICurrentUser, CurrentUser>();
    }
}
