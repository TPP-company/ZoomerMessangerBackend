namespace ZM.Application.Common.Exceptions;

/// <summary>
/// Ресурс не найден.
/// </summary>
public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException()
    { }

    public ResourceNotFoundException(string? message) : base(message)
    { }
}