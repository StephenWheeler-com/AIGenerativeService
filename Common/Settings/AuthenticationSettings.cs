namespace Demo.GenerativeAI.Common.Settings;

public class AuthenticationSettings
{
    public int JwtTokenExpireMinutes { get; set; }

    public int RefreshTokenExpireMinutes { get; set; }

    public KeyVaultSettings? KeyVaultSettings { get; set; }

    public CookieSettings? CookieSettings { get; set; }
}