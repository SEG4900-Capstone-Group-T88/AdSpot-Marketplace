using HotChocolate.Authorization;

namespace AdSpot.Api.Queries;

[QueryType]
public class ConnectionQueries
{
    [Authorize]
    public IQueryable<Connection> GetConnection(
        int userId,
        int platformId,
        ConnectionRepository repo
    )
    {
        return repo.GetConnection(userId, platformId);
    }
}
