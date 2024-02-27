namespace AdSpot.Api.Repositories;

public class ConnectionRepository
{
    private readonly AdSpotDbContext context;

    public ConnectionRepository(AdSpotDbContext context)
    {
        this.context = context;
    }

    public Connection AddConnection(Connection connection)
    {
        context.Connections.Add(connection);
        context.SaveChanges();
        return connection;
    }
}
