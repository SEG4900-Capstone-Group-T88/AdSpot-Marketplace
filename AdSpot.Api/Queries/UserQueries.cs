namespace AdSpot.Api.Queries;

[QueryType]
public class UserQueries
{
    public IQueryable<User> GetUsers(UserRepository repo)
    {
        return repo.GetAllUsers();
    }
}
