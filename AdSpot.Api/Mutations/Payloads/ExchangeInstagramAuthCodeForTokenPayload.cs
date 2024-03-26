namespace AdSpot.Api.Mutations.Payloads;

public class ExchangeInstagramAuthCodeForTokenPayload
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("user_id")]
    public long UserId { get; set; }
}
