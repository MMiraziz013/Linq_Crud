namespace Clean.Application.Services.Jwt;

public sealed class JwtOptions
{
    public const string SectionName = "JWT";
    public string Issuer { get; init; } = default!;
    public string Audience { get; init; } = default!;
    public string Key { get; init; } = default!;
    public int AccessTokenMinutes { get; init; } = 60;
}