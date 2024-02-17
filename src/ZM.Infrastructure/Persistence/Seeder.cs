using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ZM.Domain.Entities;
using ZM.Infrastructure.Authentication.Entities;
using ZM.Infrastructure.Persistence.App;

namespace ZM.Infrastructure.Persistence;
public static class Seeder
{
    private static UserManager<AuthUser> userManager;
    private static AppDbContext appDbContext;
    private static TimeProvider timeProvider;

    private const string victorUserName = "victor";
    private const string vedyaUserName = "vedya";
    private const string password = "Aa123!";

    public static  void Seed(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        userManager = scope.ServiceProvider.GetRequiredService<UserManager<AuthUser>>();
        appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();

        CreateUser(victorUserName);
        CreateUser(vedyaUserName);
    }

    private static void CreateUser(string userName)
    {
        var IsUserExist = appDbContext.Users.Any(x => x.UserName == userName);

        if (IsUserExist == false)
        {
            var authUser = new AuthUser()
            {
                UserName = userName,
            };

            var createUser = userManager.CreateAsync(authUser, password);
            createUser.Wait();

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
}
