namespace AdSpot.Api.Queries;

[QueryType]
public class UserQueries
{
    public IQueryable<User> GetUsers(UserRepository repo)
    {
        return repo.GetAllUsers();
    }

    public User ValidateUser(UserRepository repo, String email, String password)
    {
        var user = repo.ValidateUser(email, password);

        if (user is null)
        {
            //throw new IncorrectPasswordError();
        }

        return user;
    }
}

public record IncorrectPasswordError()
{
    public string Message => "Password did not match our records.";
}

public record IncorrectEmailError()
{
    public string Message => "Email did not match our records.";
}

