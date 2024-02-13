using Microsoft.AspNetCore.Http;
using ZM.Application.Dependencies.Infrastructure.Authentication;

namespace ZM.Infrastructure.Authentication;

/// <inheritdoc cref="ICurrentUser"/>
internal class CurrentUser : ICurrentUser
{
    private static readonly Guid UnknownId = Guid.Empty;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        var httpContext = httpContextAccessor.HttpContext;

        if (httpContext is null)
            return;

        Id = httpContext.User.FindFirst("id") is null ? 
            UnknownId : 
            Guid.Parse(httpContext.User.FindFirst("id")!.Value);

        if (Id != UnknownId)
            IsUnknown = false;
    }

    public Guid Id { get; } = UnknownId;
    public bool IsUnknown { get; } = true;
}
