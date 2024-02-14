namespace ZM.Common.Results;

/// <summary>
/// Информация о ошибке.
/// </summary>
public class Error : IError
{
    public Error()
    { }

    public Error(string code)
        : this(code, description: null)
    { }

    public Error(string code, string? description)
        : this(code, description, [])
    { }

    public Error(string code, Dictionary<string, string[]> reason)
        : this(code, description: null, reason)
    { }

    public Error(string code, string? description, Dictionary<string, string[]> reason)
    {
        Code = code;
        Description = description;
        Reason = reason;
    }

    public string Code { get; init; }
    public string? Description { get; init; }
    public Dictionary<string, string[]> Reason { get; init; } = [];
}