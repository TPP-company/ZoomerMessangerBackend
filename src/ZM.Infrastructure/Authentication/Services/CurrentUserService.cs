using Microsoft.AspNetCore.Http;
using ZM.Application.Dependencies.Infrastructure.Authentication;

namespace ZM.Infrastructure.Authentication.Services;
internal class CurrentUserService : ICurrentUserService
{
    private static readonly Guid UnknownId = Guid.Empty;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        var httpContext = httpContextAccessor.HttpContext;

        if (httpContext is null)
            return;

        Id = httpContext.User.FindFirst("id") is null ? UnknownId : Guid.Parse(httpContext.User.FindFirst("id")!.Value);

        if (Id != UnknownId)
            IsUnknown = false;
    }

    public Guid Id { get; } = Guid.Empty;
    public bool IsUnknown { get; } = true;
}
