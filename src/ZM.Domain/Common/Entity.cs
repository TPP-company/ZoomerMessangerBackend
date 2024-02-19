namespace ZM.Domain.Common;

/// <summary>
/// Сущность.
/// </summary>
public abstract class Entity : IHasId
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; protected set; }
}
