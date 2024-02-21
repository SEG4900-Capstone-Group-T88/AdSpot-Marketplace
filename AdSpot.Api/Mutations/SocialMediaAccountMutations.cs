namespace AdSpot.Api.Mutations;

[MutationType]
public class SocialMediaAccountMutations {
    public SocialMediaAccount AddSocialMediaAccount(string platform, string accountHandle, string apiToken, string userEmail, SocialMediaAccountRepository repo) {
        var account = repo.AddSocialMediaAccount(new SocialMediaAccount
        {   
            AccountPlatform = platform,
            AccountHandle = accountHandle,
            APIToken = apiToken,
            UserEmail = userEmail
        });

        return account;
    }
}