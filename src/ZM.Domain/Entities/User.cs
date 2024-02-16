using ZM.Domain.Common;

namespace ZM.Domain.Entities;

/// <summary>
/// Пользователь.
/// </summary>
public class User : Entity
{
    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string UserName { get; set; } = null!;

    /// <summary>
    /// Информация о.
    /// </summary>
    public string? About { get; set; }

    /// <summary>
    /// Идентификатор аватарки.
    /// </summary>
    public Guid? AvatarId { get; set; }

    /// <summary>
    /// Дата регистрации.
    /// </summary>
    public DateTime DateOfRegistration { get; set; }

    /// <summary>
    /// Идентификатор внешнего пользователя.
    /// </summary>
    public Guid ExternalId { get; set; }
}
