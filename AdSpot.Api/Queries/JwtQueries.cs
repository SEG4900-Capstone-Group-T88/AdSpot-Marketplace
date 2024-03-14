namespace AdSpot.Api.Queries;

[QueryType]
public class JwtQueries
{
    public string GetJwtToken([Service] IConfiguration config)
    {
        return JwtUtils.GenerateJSONWebToken(config);
    }
}
