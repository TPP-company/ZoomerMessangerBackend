namespace ZM.Infrastructure.Authentication.Services;

/// <summary>
/// Информация для входа.
/// </summary>
public record SignInRequest(string Username, string Password);

/// <summary>
/// Информация для регистрации.
/// </summary>
public record SignUpRequest(string Username, string Password);