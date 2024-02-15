namespace ZM.Common.Results;

/// <summary>
/// Результат.
/// </summary>
public interface IResult<TError> where TError : IError
{
    /// <summary>
    /// Является успешным.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Ошибка.
    /// </summary>
    public TError? Error { get; set; }
}
