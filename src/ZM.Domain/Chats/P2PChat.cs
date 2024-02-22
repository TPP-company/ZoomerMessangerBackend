using ZM.Domain.Common;
using ZM.Domain.Entities;

namespace ZM.Domain.Chats;

/// <summary>
/// Чат 1 на 1.
/// </summary>
public class P2PChat : Chat
{
    private P2PChat()
    { }

    public P2PChat(User user1, User user2)
    {
        Users ??= [];

        Users.Add(user1);
        Users.Add(user2);
    }

    /// <summary>
    /// Сообщения.
    /// </summary>
    public ICollection<P2PChatMessage> Messages { get; set; }
}
