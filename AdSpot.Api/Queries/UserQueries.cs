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
    public IQueryable<User>? WhoAmI(UserRepository repo, ClaimsPrincipal claimsPrincipal)
    {
        var success = int.TryParse(claimsPrincipal.FindFirstValue(JwtRegisteredClaimNames.Sub), out var userId);
        if (!success)
        {
            return null;
        }
        return repo.GetUserById(userId);
    }
}
