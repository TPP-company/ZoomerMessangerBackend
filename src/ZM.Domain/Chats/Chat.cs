using ZM.Domain.Common;
using ZM.Domain.Entities;

namespace ZM.Domain.Chats;

/// <summary>
/// Чат.
/// </summary>
public class Chat : Entity
{
    private Chat()
    { }

    public Chat(User user1, User user2)
    {
        Users ??= [];

        Users.Add(user1);
        Users.Add(user2);
    }

    /// <summary>
    /// Пользователи.
    /// </summary>
    public ICollection<User> Users { get; protected set; }

    /// <summary>
    /// Сообщения.
    /// </summary>
    public ICollection<ChatMessage> ChatMessages { get; set; }
}
