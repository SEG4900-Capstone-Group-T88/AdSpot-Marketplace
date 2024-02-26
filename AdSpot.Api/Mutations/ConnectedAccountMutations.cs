namespace AdSpot.Api.Mutations;

[MutationType]
public class ConnectedAccountMutations
{
    public ConnectedAccount AddSocialMediaAccount(int platformId, string accountHandle, string apiToken, int userId,
        ConnectedAccountRepository repo)
    {
        var account = repo.AddSocialMediaAccount(new ConnectedAccount
        {
            PlatformId = platformId,
            Handle = accountHandle,
            Token = apiToken,
            UserId = userId
        });

        return account;
    }
}
