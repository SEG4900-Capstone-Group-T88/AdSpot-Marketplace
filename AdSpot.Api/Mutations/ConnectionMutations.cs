namespace AdSpot.Api.Mutations;

[MutationType]
public class ConnectionMutations
{
    [UseProjection]
    public IQueryable<Connection> AddConnection(int platformId, string accountHandle, string apiToken, int userId,
        ConnectionRepository repo)
    {
        var account = repo.AddConnection(new Connection
        {
            PlatformId = platformId,
            Handle = accountHandle,
            Token = apiToken,
            UserId = userId
        });

        return account;
    }

    [Error<InstagramOauthError>]
    public async Task<MutationResult<ExchangeInstagramAuthCodeForTokenPayload>> ExchangeInstagramAuthCodeForToken(
        //int userId,
        //int platformId,
        string authCode,
        InstagramService service,
        ConnectionRepository repo)
    {
        var response = await service.ExchangeAuthCodeForToken(authCode);
        var json = await response.Content.ReadFromJsonAsync<ExchangeInstagramAuthCodeForTokenPayload>();
        if (json?.AccessToken is null)
        {
            var content = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<JObject>(content);
            return new(new InstagramOauthError(error));
        }
        //repo.AddConnection(new Connection
        //{
        //    PlatformId = platformId,
        //    Token = json.AccessToken,
        //    UserId = userId
        //});
        return json;
    }
}
