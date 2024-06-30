using HotChocolate.Authorization;
using HotChocolate.Subscriptions;

namespace AdSpot.Api.Mutations;

[MutationType]
public class ConnectionMutations
{
    [Authorize]
    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<Connection> AddConnection(
        int userId,
        int platformId,
        string accountHandle,
        string apiToken,
        ConnectionRepository repo
    )
    {
        var account = repo.AddConnection(
            new Connection
            {
                PlatformId = platformId,
                Handle = accountHandle,
                Token = apiToken,
                UserId = userId
            }
        );

        return account;
    }

    [Error<InstagramOauthError>]
    public async Task<MutationResult<Connection?>> ExchangeInstagramAuthCodeForToken(
        int userId,
        int platformId,
        string authCode,
        InstagramService service,
        ConnectionRepository repo,
        [Service] ITopicEventSender topicEventSender
    )
    {
        var response = await service.ExchangeAuthCodeForAccessToken(authCode);
        var json = await response.Content.ReadFromJsonAsync<ExchangeInstagramAuthCodeForTokenPayload>();
        if (json?.AccessToken is null)
        {
            var content = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<JObject>(content);
            return new(new InstagramOauthError(error!));
        }

        var tokenResponse = await service.GetLongLivedToken(json.AccessToken);
        var token = tokenResponse?.Value<string>("access_token");
        if (tokenResponse is not null && token is not null)
        {
            var user = await service.GetUser(token);
            var handle = user?.Value<string>("username");

            if (handle is not null)
            {
                var expiresIn = tokenResponse.Value<int>("expires_in");
                var expirationDate = DateTime.UtcNow.AddSeconds(expiresIn);

                var connection = repo.AddOrUpdateConnection(
                        new Connection
                        {
                            Handle = handle,
                            PlatformId = platformId,
                            Token = token,
                            TokenExpiration = expirationDate,
                            UserId = userId
                        }
                    )
                    .FirstOrDefault();

                var topicName = $"{userId}_{nameof(Subscription.OnAccountConnected)}";
                await topicEventSender.SendAsync(topicName, connection);

                return connection;
            }
        }

        return null;
    }
}
