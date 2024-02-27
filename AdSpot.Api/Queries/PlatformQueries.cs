namespace AdSpot.Api.Queries;

[QueryType]
public class PlatformQueries
{
    [UseProjection]
    public IQueryable<Platform> GetPlatforms(PlatformRepository repo)
    {
        return repo.GetAllPlatforms();
    }
}
