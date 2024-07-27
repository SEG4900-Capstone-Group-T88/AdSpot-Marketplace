namespace AdSpot.Api.Queries;

[QueryType]
public class UserQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<User> GetUsers(UserRepository repo)
    {
        return repo.GetAllUsers();
    }

    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<User> GetUserById(int userId, UserRepository repo)
    {
        return repo.GetUserById(userId);
    }

    [UseFirstOrDefault]
    [UseProjection]
    public async Task<IQueryable<User>?> WhoAmI(UserRepository repo, IHttpContextAccessor httpContextAccessor)
    {
        var token = await httpContextAccessor.HttpContext.GetTokenAsync("access_token");
        if (token is null)
        {
            return null;
        }
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);
        var userId = int.Parse(jwt.Subject);
        return repo.GetUserById(userId);
    }
}
