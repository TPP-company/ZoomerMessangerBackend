using ZM.Domain.Users;

namespace ZM.Domain.Common;

/// <summary>
/// Чат.
/// </summary>
public abstract class Chat : Entity
{
	/// <summary>
	/// Пользователи.
	/// </summary>
	public ICollection<User> Users { get; protected set; }
}
