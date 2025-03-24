namespace DistribuTe.Framework.ApiEssentials.Auth;

public class AuthSettings
{
    public string JwtAudience { get; set; } = null!;
    public string JwtIssuer { get; set; } = null!;
}