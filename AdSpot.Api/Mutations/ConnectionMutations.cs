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
    public async Task<MutationResult<Connection?>> ExchangeInstagramAuthCodeForToken(
        int userId,
        int platformId,
        string authCode,
        InstagramService service,
        ConnectionRepository repo)
    {
        var response = await service.ExchangeAuthCodeForAccessToken(authCode);
        var json = await response.Content.ReadFromJsonAsync<ExchangeInstagramAuthCodeForTokenPayload>();
        if (json?.AccessToken is null)
        {
            var content = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<JObject>(content);
            return new(new InstagramOauthError(error));
        }

        var tokenResponse = await service.GetLongLivedToken(json.AccessToken);
        var token = tokenResponse["access_token"]?.ToString();
        if (token is not null)
        {
            var user = await service.GetUser(token);
            var handle = user["username"].ToString();

            var expiresIn = tokenResponse.Value<int>("expires_in");
            var expirationDate = DateTime.UtcNow.AddSeconds(expiresIn);

            var connection = repo.AddOrUpdateConnection(new Connection
            {
                Handle = handle,
                PlatformId = platformId,
                Token = token,
                TokenExpiration = expirationDate,
                UserId = userId
            });
            return connection.FirstOrDefault();
        }

        return null;
    }
}
