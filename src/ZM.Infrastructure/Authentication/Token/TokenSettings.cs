using System.ComponentModel.DataAnnotations;

namespace ZM.Infrastructure.Authentication.Token;

/// <summary>
/// Опции токена.
/// </summary>
internal class TokenSettings
{
    [Required]
    public string Audience { get; set; } = null!;

    [Required]
    public string Issuer { get; set; } = null!;

    [Required]
    public string Secret { get; set; } = null!;

    [Required]
    public int ExpiresInMinutes { get; set; }
}
