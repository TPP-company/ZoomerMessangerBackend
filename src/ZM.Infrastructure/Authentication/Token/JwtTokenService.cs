using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ZM.Infrastructure.Authentication.Entities;
using ZM.Application.Dependencies.Infrastructure.Authentication;

namespace ZM.Infrastructure.Authentication.Token;

/// <summary>
/// Сервис JWT токена.
/// </summary>
internal class JwtTokenService(TokenSettings _tokenSettings) : ITokenService
{
    public TokenDto Generate(AuthUser authUser)
    {
        var claims = CreateClaims(authUser);
        var token = CreateJwtSecurityToken(claims);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new TokenDto(tokenString);
    }

    private JwtSecurityToken CreateJwtSecurityToken(List<Claim> claims)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Secret));
        var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        return new JwtSecurityToken(
            issuer: _tokenSettings.Issuer,
            audience: _tokenSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_tokenSettings.ExpiresInMinutes),
            signingCredentials: creds);
    }

    private static List<Claim> CreateClaims(AuthUser authUser)
    {
        return [
            new Claim(ClaimTypes.Name, authUser.UserName!),
            new Claim(KnownClaims.ExternalId, authUser.Id.ToString())];
    }
}
