namespace AdSpot.Api.Repositories;

public class ConnectionRepository
{
    private readonly AdSpotDbContext context;

    public ConnectionRepository(AdSpotDbContext context)
    {
        this.context = context;
    }

    public IQueryable<Connection> GetConnections(int userId)
    {
        return context.Connections.Where(c => c.UserId == userId);
    }

    public IQueryable<Connection> GetConnection(int userId, int platformId)
    {
        return context.Connections.Where(x => x.UserId == userId && x.PlatformId == platformId);
    }

    public Connection AddConnection(Connection connection)
    {
        context.Connections.Add(connection);
        context.SaveChanges();
        return context.Connections.First(x => x.UserId == connection.UserId && x.PlatformId == connection.PlatformId);
    }

    public Connection UpdateConnection(Connection connection)
    {
        var existingConnection = context.Connections.First(x =>
            x.UserId == connection.UserId && x.PlatformId == connection.PlatformId
        );
        existingConnection.Token = connection.Token;
        existingConnection.TokenExpiration = connection.TokenExpiration;
        existingConnection.Handle = connection.Handle;
        context.SaveChanges();

        return context.Connections.First(x => x.UserId == connection.UserId && x.PlatformId == connection.PlatformId);
    }

    public Connection AddOrUpdateConnection(Connection connection)
    {
        var existingConnection = context.Connections.FirstOrDefault(x =>
            x.UserId == connection.UserId && x.PlatformId == connection.PlatformId
        );
        if (existingConnection is null)
        {
            return AddConnection(connection);
        }
        else
        {
            return UpdateConnection(connection);
        }
    }
}
