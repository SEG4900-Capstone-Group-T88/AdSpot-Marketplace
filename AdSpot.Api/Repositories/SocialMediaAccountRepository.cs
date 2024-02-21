namespace AdSpot.Api.Repositories;

public class SocialMediaAccountRepository
{
    private readonly AdSpotDbContext context;

    public SocialMediaAccountRepository(AdSpotDbContext context)
    {
        this.context = context;
    }

    public SocialMediaAccount AddSocialMediaAccount(SocialMediaAccount mediaAccount)
    {
        context.SocialMediaAccounts.Add(mediaAccount);
        context.SaveChanges();
        return mediaAccount;
    }
}