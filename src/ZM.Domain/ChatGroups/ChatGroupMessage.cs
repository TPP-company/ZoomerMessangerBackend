using ZM.Domain.Common;
using ZM.Domain.Entities;

namespace ZM.Domain.ChatGroups;

/// <summary>
/// Сообщение чат группы.
/// </summary>
public class ChatGroupMessage : Entity
{
    private ChatGroupMessage()
    { }

    public ChatGroupMessage(string content, DateTime createdDate, Guid senderId, Guid chatGroupId)
    {
        Content = content;
        CreatedDate = createdDate;
        SenderId = senderId;
        ChatGroupId = chatGroupId;
    }

    /// <summary>
    /// Сообщение.
    /// </summary>
    public string Content { get; protected set; } = null!;

    /// <summary>
    /// Дата создания.
    /// </summary>
    public DateTime CreatedDate { get; protected set; }

    /// <summary>
    /// Идентификатор отправителя.
    /// </summary>
    public Guid SenderId { get; protected set; }

    /// <summary>
    /// Отправитель.
    /// </summary>
    public User Sender { get; protected set; }

    /// <summary>
    /// Идентификатор чат группы.
    /// </summary>
    public Guid ChatGroupId { get; protected set; }

    /// <summary>
    /// Чат группа.
    /// </summary>
    public ChatGroup ChatGroup { get; protected set; }

    /// <summary>
    /// Сообщение прочитано.
    /// </summary>
    public bool HasBeenRead { get; set; }
}
