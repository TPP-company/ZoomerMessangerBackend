namespace ZM.Infrastructure.Authentication.Services;
public record SignInRequest(string Username, string Password);

public record SignUpRequest(string Username, string Password);