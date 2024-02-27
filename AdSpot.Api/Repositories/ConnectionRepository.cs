namespace AdSpot.Api.Repositories;

public class ConnectionRepository
{
    private readonly AdSpotDbContext context;

    public ConnectionRepository(AdSpotDbContext context)
    {
        this.context = context;
    }

    public IQueryable<Connection> AddConnection(Connection connection)
    {
        context.Connections.Add(connection);
        var id = context.SaveChanges();
        return context.Connections.Where(x => x.ConnectionId == id);
    }
}
