namespace AdSpot.Api.Services;

public class InstagramService
{
    private readonly HttpClient httpClient;
    private readonly IConfiguration config;

    public InstagramService(HttpClient httpClient, IConfiguration config)
    {
        this.httpClient = httpClient;
        this.httpClient.BaseAddress = new Uri("https://api.instagram.com/oauth/access_token");

        this.config = config;
    }

    public async Task<HttpResponseMessage> ExchangeAuthCodeForToken(string code)
    {
        var body = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "client_id", config["OAuth:Instagram:ClientId"] },
            { "client_secret", config["OAuth:Instagram:ClientSecret"] },
            { "grant_type", "authorization_code" },
            { "redirect_uri", config["OAuth:Instagram:RedirectUri"] },
            { "code", code }
        });

        var response = await httpClient.PostAsync("", body);
        return response;
    }
}
