using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ZM.Infrastructure.Authentication.Entities;

namespace ZM.Infrastructure.Authentication.Token;

/// <inheritdoc cref="IJwtTokenService"/>
public class JwtTokenService : IJwtTokenService
{
    public TokenDto Generate(AuthUser authUser)
    {
        List<Claim> claims = [
            new Claim(ClaimTypes.Name, authUser.UserName!),
            new Claim("id", authUser.Id.ToString()),
            ];

        //TODO: Вынести секрет в конфиг
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("supersecret123123adminmazerratty!!!"));
        var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //TODO: Вынести конфигурацию токена в конфиг
        var token = new JwtSecurityToken(
            issuer: "zm-backend",
            audience: "zm-flatter",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: creds);

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenString = tokenHandler.WriteToken(token);

        return new TokenDto(tokenString);
    }
}
