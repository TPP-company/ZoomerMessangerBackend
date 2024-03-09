using ZM.Domain.Common;
using ZM.Domain.Users;

namespace ZM.Domain.Chats;

/// <summary>
/// Сообщение чата 1 на 1.
/// </summary>
public class P2PChatMessage : Entity
{
	private P2PChatMessage()
	{ }

	public P2PChatMessage(string content, DateTime createdDate, Guid senderId, Guid chatId)
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
	/// Идентификатор чата 1 на 1.
	/// </summary>
	public Guid ChatId { get; protected set; }

	/// <summary>
	/// Чат 1 на 1.
	/// </summary>
	public P2PChat Chat { get; protected set; }

	/// <summary>
	/// Сообщение прочитано.
	/// </summary>
	public bool HasBeenRead { get; set; }
}