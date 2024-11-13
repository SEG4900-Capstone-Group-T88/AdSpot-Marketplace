namespace AdSpot.Api.Queries;

[QueryType]
public class FlairQueries
{
    [Authorize]
    public IQueryable<Flair> GetFlairs(int userId, FlairRepository flairRepo)
    {
        return flairRepo.GetFlairs(userId);
    }
}
