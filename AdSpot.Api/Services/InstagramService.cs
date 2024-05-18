namespace AdSpot.Api.Services;

public class InstagramService
{
    // https://developers.facebook.com/docs/instagram-basic-display-api/guides/long-lived-access-tokens

    private readonly HttpClient apiClient;
    private readonly HttpClient graphClient;
    private readonly IOptions<OAuthOptions> oauthOptions;

    public InstagramService(IHttpClientFactory httpClientFactory, IOptions<OAuthOptions> oauthOptions)
    {
        apiClient = httpClientFactory.CreateClient();
        apiClient.BaseAddress = new Uri("https://api.instagram.com/");

        graphClient = httpClientFactory.CreateClient();
        graphClient.BaseAddress = new Uri("https://graph.instagram.com/");

        this.oauthOptions = oauthOptions;
    }

    public async Task<HttpResponseMessage> ExchangeAuthCodeForAccessToken(string code)
    {
        var body = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "client_id", oauthOptions.Value.Instagram.ClientId },
            { "client_secret", oauthOptions.Value.Instagram.ClientSecret },
            { "grant_type", "authorization_code" },
            { "redirect_uri", oauthOptions.Value.Instagram.RedirectUri },
            { "code", code }
        });

        var response = await apiClient.PostAsync("oauth/access_token", body);
        return response;
    }

    public async Task<JObject?> GetUser(string accessToken)
    {
        var parameters = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "fields", "id,username" },
            { "access_token", accessToken }
        });
        var query = await parameters.ReadAsStringAsync();
        var response = await graphClient.GetAsync("me?" + query);
        var content = await response.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<JObject>(content);
        return obj;
    }

    public async Task<JObject?> GetLongLivedToken(string accessToken)
    {
        var parameters = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "grant_type", "ig_exchange_token" },
            { "client_secret", oauthOptions.Value.Instagram.ClientSecret },
            { "access_token", accessToken }
        });
        var query = await parameters.ReadAsStringAsync();
        var response = await graphClient.GetAsync("access_token?" + query);
        var content = await response.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<JObject>(content);
        return obj;
    }

    public async Task<JObject?> RefreshToken(string accessToken)
    {
        var parameters = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "grant_type", "ig_refresh_token" },
            { "access_token", accessToken }
        });
        var query = await parameters.ReadAsStringAsync();
        var response = await graphClient.GetAsync("refresh_access_token?" + query);
        var content = await response.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<JObject>(content);
        return obj;
    }
}
