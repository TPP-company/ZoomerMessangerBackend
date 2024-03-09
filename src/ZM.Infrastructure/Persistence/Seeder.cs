using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text;
using ZM.Domain.Chats;
using ZM.Domain.Users;
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
	private const string ismailUserName = "ismail";
	private const string maflendUserName = "maflend";
	private const string password = "Aa123!";

	public static void Seed(IServiceProvider serviceProvider)
	{
		using var scope = serviceProvider.CreateScope();

		userManager = scope.ServiceProvider.GetRequiredService<UserManager<AuthUser>>();
		appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
		timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();
		logger = scope.ServiceProvider.GetRequiredService<ILogger<Seeder>>();

		var victorId = CreateUser(victorUserName);
		var vedyaId = CreateUser(vedyaUserName);
		var ismailId = CreateUser(ismailUserName);
		var maflendId = CreateUser(maflendUserName);

		CreateP2PChat(vedyaId, ismailId);
		CreateP2PChat(ismailId, victorId);
		CreateP2PChat(ismailId, maflendId);
	}

    private static void CreateP2PChat(Guid userId1, Guid userId2)
    {
        Guid[] users = [userId1, userId2];
        var isChatUserExist = appDbContext.P2PChats.Any(x => x.Members.All(user => users.Contains(user.Id)));
        if (isChatUserExist) return;

		var user1 = appDbContext.Users.First(x => x.Id == userId1);
		var user2 = appDbContext.Users.First(x => x.Id == userId2);

		var chat = new P2PChat(user1, user2);

		appDbContext.P2PChats.Add(chat);

		var chatMessages = new List<P2PChatMessage>()
		{
			new ("Задает для _myAllowSpecificOriginsимени политики значение . Имя политики является произвольным.", timeProvider.GetUtcNow().UtcDateTime, userId1, chat.Id),
			new ("UseCors Вызывает метод расширения и задает _myAllowSpecificOrigins политику CORS. UseCors добавляет ПО промежуточного слоя CORS. UseCors Вызов должен быть помещен после UseRouting, но до UseAuthorization. Дополнительные сведения см. в порядке по промежуточного слоя. ", timeProvider.GetUtcNow().UtcDateTime, userId1, chat.Id),
			new ("Вызовы AddCors с лямбда-выражением. Лямбда принимает CorsPolicyBuilder объект. Параметры конфигурации, например WithOrigins, описаны далее в этой статье. ", timeProvider.GetUtcNow().UtcDateTime, userId1, chat.Id),
			new ("_myAllowSpecificOrigins Включает политику CORS для всех конечных точек контроллера. См. маршрутизацию конечных точек, чтобы применить политику CORS к определенным конечным точкам.", timeProvider.GetUtcNow().UtcDateTime, userId2, chat.Id),
			new ("При использовании по промежуточного слоя кэширования ответов вызовите UseCors раньше UseResponseCaching.", timeProvider.GetUtcNow().UtcDateTime, userId2, chat.Id),
			new ("Вызов AddCors метода", timeProvider.GetUtcNow().UtcDateTime, userId1, chat.Id),
			new ("добавляет службы CORS", timeProvider.GetUtcNow().UtcDateTime, userId1, chat.Id),
			new ("в контейнер службы", timeProvider.GetUtcNow().UtcDateTime, userId2, chat.Id),
			new ("приложения:", timeProvider.GetUtcNow().UtcDateTime, userId2, chat.Id),
		};

		appDbContext.P2PChatMessages.AddRange(chatMessages);
		appDbContext.SaveChanges();
	}

	private static Guid CreateUser(string userName)
	{
		var user = appDbContext.Users.FirstOrDefault(u => u.UserName == userName);
		if (user is not null) return user.Id;

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
			throw new Exception(errorBuilder.ToString());
		}

		var newUser = new User()
		{
			ExternalId = authUser.Id,
			UserName = authUser.UserName,
			DateOfRegistration = timeProvider.GetUtcNow().UtcDateTime,
		};

		appDbContext.Users.Add(newUser);
		appDbContext.SaveChanges();
		return newUser.Id;
	}
}
