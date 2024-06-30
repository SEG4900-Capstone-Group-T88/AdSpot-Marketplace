namespace AdSpot.Api.Repositories;

public class ConnectionRepository
{
    private readonly AdSpotDbContext context;

    public ConnectionRepository(AdSpotDbContext context)
    {
        this.context = context;
    }

    public IQueryable<Connection> GetConnections()
    {
        return context.Connections;
    }

    public IQueryable<Connection> GetConnection(int userId, int platformId)
    {
        return context.Connections.Where(x => x.UserId == userId && x.PlatformId == platformId);
    }

    public IQueryable<Connection> AddConnection(Connection connection)
    {
        context.Connections.Add(connection);
        context.SaveChanges();
        return context.Connections.Where(x => x.UserId == connection.UserId && x.PlatformId == connection.PlatformId);
    }

    public IQueryable<Connection> UpdateConnection(Connection connection)
    {
        var existingConnection = context.Connections.FirstOrDefault(x =>
            x.UserId == connection.UserId && x.PlatformId == connection.PlatformId
        );
        existingConnection.Token = connection.Token;
        existingConnection.TokenExpiration = connection.TokenExpiration;
        existingConnection.Handle = connection.Handle;
        context.SaveChanges();

        return context.Connections.Where(x => x.UserId == connection.UserId && x.PlatformId == connection.PlatformId);
    }

    public IQueryable<Connection> AddOrUpdateConnection(Connection connection)
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
