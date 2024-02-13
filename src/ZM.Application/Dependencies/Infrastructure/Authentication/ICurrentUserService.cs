namespace ZM.Application.Dependencies.Infrastructure.Authentication;
public interface ICurrentUserService
{
    public Guid Id { get; }
    public bool IsUnknown { get; }
}
