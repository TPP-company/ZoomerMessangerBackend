namespace ZM.Common.Hubs;

/// <summary>
/// Роуты хабов.
/// </summary>
public class HubsRoutes
{
	/// <summary>
	/// Базовый url.
	/// </summary>
	public const string BaseUrl = "/hubs";

	/// <summary>
	/// Url хабов чатов.
	/// </summary>
	public const string ChatsHubs = BaseUrl + "/chats";

	/// <summary>
	/// Url хаба p2p чата.
	/// </summary>
	public const string P2PChatsHubs = ChatsHubs + "/p2p";

	/// <summary>
	/// Название query параметр для токена аутентификации.
	/// </summary>
	public const string TokenQueryParameter = "token";
}
