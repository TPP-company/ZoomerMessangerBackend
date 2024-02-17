using ZM.Domain.Common;
using ZM.Domain.Entities;

namespace ZM.Domain.ChatGroups;

/// <summary>
/// Группа.
/// </summary>
public class ChatGroup : Entity
{
    public ChatGroup(string name, Guid creatorId)
    {
        Name = name;
        CreatorId = creatorId;
    }

    /// <summary>
    /// Название.
    /// </summary>
    public string Name { get; protected set; }

    /// <summary>
    /// Идентификатор создателя.
    /// </summary>
    public Guid CreatorId { get; protected set; }

    /// <summary>
    /// Создатель.
    /// </summary>
    public User Creator { get; protected set; }

    /// <summary>
    /// Пользователи.
    /// </summary>
    public ICollection<User> Users { get; protected set; }

    /// <summary>
    /// Сообщения.
    /// </summary>
    public ICollection<ChatGroupMessage> Messages { get; protected set; }
}
