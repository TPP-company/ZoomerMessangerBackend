using ZM.Domain.Common;
using ZM.Domain.Entities;

namespace ZM.Domain.Chats;

/// <summary>
/// Сообщение чата.
/// </summary>
public class ChatMessage : Entity
{
    private ChatMessage()
    { }

    public ChatMessage(string content, DateTime createdDate, Guid senderId, Guid chatId)
    {
        Content = content;
        CreatedDate = createdDate;
        SenderId = senderId;
        ChatId = chatId;
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
    /// Идентификатор чата.
    /// </summary>
    public Guid ChatId { get; protected set; }

    /// <summary>
    /// Чат.
    /// </summary>
    public Chat Chat { get; protected set; }

    /// <summary>
    /// Сообщение прочитано.
    /// </summary>
    public bool HasBeenRead { get; set; }
}