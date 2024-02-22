namespace AdSpot.Api.Mutations;

[MutationType]
public class ConnectedAccountMutations
{
    public ConnectedAccount AddSocialMediaAccount(string platform, string accountHandle, string apiToken, int userId,
        ConnectedAccountRepository repo)
    {
        var account = repo.AddSocialMediaAccount(new ConnectedAccount
        {
            Platform = platform,
            Handle = accountHandle,
            Token = apiToken,
            UserId = userId
        });

        return account;
    }
}