using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text;
using ZM.Domain.Entities;
using ZM.Infrastructure.Authentication.Entities;
using ZM.Infrastructure.Persistence.App;

namespace ZM.Infrastructure.Persistence;
public class Seeder
{
    private static UserManager<AuthUser> userManager = null!;
    private static AppDbContext appDbContext = null!;
    private static TimeProvider timeProvider = null!;
    private static ILogger<Seeder> logger = null!;

    private const string victorUserName = "victor";
    private const string vedyaUserName = "vedya";
    private const string password = "Aa123!";

    public static void Seed(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        userManager = scope.ServiceProvider.GetRequiredService<UserManager<AuthUser>>();
        appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();
        logger = scope.ServiceProvider.GetRequiredService<ILogger<Seeder>>();

        CreateUser(victorUserName);
        CreateUser(vedyaUserName);
    }

    private static void CreateUser(string userName)
    {
        var isUserExist = appDbContext.Users.Any(x => x.UserName == userName);
        if (isUserExist) return;

        var authUser = new AuthUser()
        {
            UserName = userName,
        };

        var createUser = userManager.CreateAsync(authUser, password);
        createUser.Wait();
        
        if (!createUser.Result.Succeeded)
        {
            var errorBuilder = new StringBuilder();

            createUser.Result.Errors
                .Select(x => x.Code + " " + x.Description)
                .ToList()
                .ForEach(error => errorBuilder.AppendLine(error));
            
            logger.LogError(errorBuilder.ToString());
            return;
        }

        var user = new User()
        {
            ExternalId = authUser.Id,
            UserName = authUser.UserName,
            DateOfRegistration = timeProvider.GetUtcNow().UtcDateTime,
        };

        appDbContext.Users.Add(user);
        appDbContext.SaveChanges();
    }
}
