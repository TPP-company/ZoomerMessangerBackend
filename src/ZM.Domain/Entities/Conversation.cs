using ZM.Domain.Common;

namespace ZM.Domain.Entities;

/// <summary>
/// Беседа.
/// </summary>
public class Conversation : Entity
{
    /// <summary>
    /// Сообщение.
    /// </summary>
    public string Message { get; set; } = null!;

    /// <summary>
    /// Дата создания.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    public bool IsNew { get; set; }

    public Guid SenderUserId { get; set; }
    public Guid ReceiverUserId { get; set; }

    public User SenderUser { get; set; }
    public User ReceiverUser { get; set; }
}