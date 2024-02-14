namespace ZM.Common.Results;

/// <inheritdoc/>
public class Result<TData> : Result<TData, Error>
{
    public Result() { }

    internal Result(TData value) : base(value)
    { }
    internal Result(Error error) : base(error)
    { }

    /// <summary>
    /// Создать неудачей результат.
    /// </summary>
    /// <param name="code">Код ошибки.</param>
    /// <returns>Неудачный результат.</returns>
    public new static Result<TData> Fail(string code)
        => new(new Error(code));

    public static implicit operator Result<TData>(TData value)
        => new(value);
}


/// <summary>
/// Результат.
/// </summary>
/// <typeparam name="TData">Данные.</typeparam>
/// <typeparam name="TError">Ошибка.</typeparam>
public class Result<TData, TError> : IResult<TError>
    where TError : class, IError, new()
{
    protected bool _success;

    public Result() { }

    internal Result(TData value)
    {
        _success = true;
        Data = value;
    }

    internal Result(TError error)
    {
        _success = false;
        Error = error;
    }

    /// <summary>
    /// Данные.
    /// </summary>
    public TData? Data { get; set; }

    /// <summary>
    /// Ошибка.
    /// </summary>
    public TError? Error { get; set; }

    /// <summary>
    /// Является успешным.
    /// </summary>
    public bool IsSuccess => _success;

    /// <summary>
    /// Является неудачей.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Создать неудачей результат.
    /// </summary>
    /// <param name="error">Ошибка.</param>
    /// <returns>Неудачный результат.</returns>
    public static Result<TData, TError> Fail(TError error)
        => new(error);

    /// <summary>
    /// Создать неудачей результат.
    /// </summary>
    /// <param name="code">Код ошибки.</param>
    /// <returns>Неудачный результат.</returns>
    public static Result<TData, TError> Fail(string code)
        => new(new TError() { Code = code });

    /// <summary>
    /// Создать успешный результат.
    /// </summary>
    /// <param name="success">Данные.</param>
    /// <returns>Успешный результат.</returns>
    public static Result<TData> Success(TData success)
        => new(success);

    public static implicit operator Result<TData, TError>(TData value)
        => new(value);
}
