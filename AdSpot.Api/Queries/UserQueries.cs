namespace AdSpot.Api.Queries;

[QueryType]
public class UserQueries
{
    [Authorize]
    [UseProjection]
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

    public User ValidateUser(string email, string password, UserRepository repo)
    {
        var user = repo.GetUserByEmail(email);

        if (user is null)
        {
            var error = ErrorBuilder.New()
                .SetCode("U01")
                .SetMessage("Invalid email")
                .Build();
            throw new GraphQLException(error);
        }

        user = repo.ValidateUser(email, password);
        if (user is null)
        {
            var error = ErrorBuilder.New()
                .SetCode("U02")
                .SetMessage("Invalid password")
                .Build();
            throw new GraphQLException(error);
        }

        return user;
    }
}
