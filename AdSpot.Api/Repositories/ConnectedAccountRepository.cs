namespace AdSpot.Api.Repositories;

public class ConnectedAccountRepository
{
    private readonly AdSpotDbContext context;

    public ConnectedAccountRepository(AdSpotDbContext context)
    {
        this.context = context;
    }

    public ConnectedAccount AddSocialMediaAccount(ConnectedAccount mediaAccount)
    {
        var user = context.Users.FirstOrDefault(u => u.UserId == mediaAccount.UserId);
        mediaAccount.User = user;

        context.ConnectedAccounts.Add(mediaAccount);
        context.SaveChanges();
        return mediaAccount;
    }
}