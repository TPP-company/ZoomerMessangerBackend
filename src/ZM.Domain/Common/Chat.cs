using ZM.Domain.Users;

namespace ZM.Domain.Common;

/// <summary>
/// Чат.
/// </summary>
public abstract class Chat : Entity
{
    /// <summary>
    /// Участники.
    /// </summary>
    public ICollection<User> Members { get; protected set; }
}
