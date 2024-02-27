namespace AdSpot.Api.Repositories;

public class PlatformRepository
{
    private readonly AdSpotDbContext context;

    public PlatformRepository(AdSpotDbContext context)
    {
        this.context = context;
    }

    public IQueryable<Platform> GetAllPlatforms()
    {
        return context.Platforms;
    }
}
