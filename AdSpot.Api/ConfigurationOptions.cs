namespace AdSpot.Api;

public class EndpointsOptions
{
    [Required]
    public string AdSpotClient { get; set; }
}

public class JwtOptions
{
    [Required]
    public string Key { get; set; }

    [Required]
    public string Issuer { get; set; }

    [Required]
    public string Audience { get; set; }
}

public class OAuthOptions
{
    [Required]
    [ValidateObjectMembers]
    public OAuthParameters Facebook { get; set; }

    [Required]
    [ValidateObjectMembers]
    public OAuthParameters Instagram { get; set; }
}

public class OAuthParameters
{
    [Required]
    public string ClientId { get; set; }

    [Required]
    public string ClientSecret { get; set; }

    [Required]
    public string RedirectUri { get; set; }
}
