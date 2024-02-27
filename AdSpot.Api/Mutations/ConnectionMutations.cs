namespace AdSpot.Api.Mutations;

[MutationType]
public class ConnectionMutations
{
    public Connection AddConnection(int platformId, string accountHandle, string apiToken, int userId,
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
}
