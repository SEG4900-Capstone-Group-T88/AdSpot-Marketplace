namespace AdSpot.Api.Queries;

[QueryType]
public class UserQueries
{
    [UseProjection]
    [UseFiltering]
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
}
