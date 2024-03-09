using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
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

		Id = httpContext.User.FindFirst(JwtRegisteredClaimNames.Sub) is null ?
			UnknownId :
			Guid.Parse(httpContext.User.FindFirst(JwtRegisteredClaimNames.Sub)!.Value);

		ExternalId = httpContext.User.FindFirst(KnownClaims.ExternalId) is null ?
			UnknownId :
			Guid.Parse(httpContext.User.FindFirst(KnownClaims.ExternalId)!.Value);

		if (ExternalId != UnknownId)
			IsUnknown = false;
	}

	public Guid Id { get; } = UnknownId;
	public Guid ExternalId { get; } = UnknownId;
	public bool IsUnknown { get; } = true;
}
