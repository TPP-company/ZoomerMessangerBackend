namespace ZM.Common.Results;

/// <summary>
/// Ошибка.
/// </summary>
public interface IError
{
	/// <summary>
	/// Код.
	/// </summary>
	string Code { get; init; }

	/// <summary>
	/// Описание.
	/// </summary>
	string? Description { get; init; }

	/// <summary>
	/// Причина.
	/// </summary>
	public Dictionary<string, string[]> Reason { get; init; }
}
