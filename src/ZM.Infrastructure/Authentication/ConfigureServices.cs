using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ZM.Application.Dependencies.Infrastructure.Authentication;
using ZM.Common.Extensions;
using ZM.Common.Hubs;
using ZM.Infrastructure.Authentication.Entities;
using ZM.Infrastructure.Authentication.Services;
using ZM.Infrastructure.Authentication.Token;
using ZM.Infrastructure.Persistence.Identity;

namespace ZM.Infrastructure.Authentication;
public static class ConfigureServices
{
	/// <summary>
	/// Добавить аутентификацию.
	/// </summary>
	public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddIdentity<AuthUser, AuthRole>(opt => { })
			.AddEntityFrameworkStores<AuthDbContext>();

		var tokenSettings = configuration
			.GetSection(nameof(TokenSettings))
			.Get<TokenSettings>() ?? throw new Exception();

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
					ValidIssuer = tokenSettings.Issuer,
					ValidAudience = tokenSettings.Audience,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Secret)),
				};

				opt.Events = new JwtBearerEvents
				{
					OnMessageReceived = context =>
					{
						if (context.Request.Path.Value!.StartsWith(HubsRoutes.BaseUrl) && 
							context.Request.Query.TryGetValue(HubsRoutes.TokenQueryParameter, out var token))
						{
							context.Token = token;
						}

						return Task.CompletedTask;
					}
				};
			});

		services.AddScoped<IAuthenticationService, AuthenticationService>();
		services.AddScoped<ITokenService, JwtTokenService>();
		services.AddScoped<ICurrentUser, CurrentUser>();
		services.RegisterOptions<TokenSettings>(configuration);
	}
}
