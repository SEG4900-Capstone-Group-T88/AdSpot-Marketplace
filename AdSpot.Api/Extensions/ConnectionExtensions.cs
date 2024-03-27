namespace AdSpot.Api.Extensions;

[ExtendObjectType<Connection>]
public class ConnectionExtensions
{
    // We can only uncomment this when all tokens in the database are valid
    //public async Task<string> GetHandle([Parent] Connection connection, InstagramService service)
    //{
    //    var user = await service.GetUser(connection.Token);
    //    return user["username"].ToString();
    //}
}
