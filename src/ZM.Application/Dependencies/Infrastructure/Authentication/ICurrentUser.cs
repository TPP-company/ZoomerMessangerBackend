namespace ZM.Application.Dependencies.Infrastructure.Authentication;

/// <summary>
/// Текущий пользователь.
/// </summary>
public interface ICurrentUser
{
	/// <summary>
	/// Идентификатор.
	/// </summary>
	public Guid Id { get; }

	/// <summary>
	/// External Идентификатор.
	/// </summary>
	public Guid ExternalId { get; }

	/// <summary>
	/// Текущего пользователь неизвестен.
	/// </summary>
	public bool IsUnknown { get; }
}
