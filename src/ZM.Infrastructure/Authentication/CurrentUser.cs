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

        ExternalId = httpContext.User.FindFirst(KnownClaims.ExternalId) is null ? 
            UnknownId : 
            Guid.Parse(httpContext.User.FindFirst(KnownClaims.ExternalId)!.Value);

        if (ExternalId != UnknownId)
            IsUnknown = false;
    }

    public Guid ExternalId { get; } = UnknownId;
    public bool IsUnknown { get; } = true;
}
