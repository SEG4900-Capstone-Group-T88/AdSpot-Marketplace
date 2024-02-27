namespace AdSpot.Api.Mutations;

[MutationType]
public class UserMutations
{
    public User AddUser(string email, string password, UserRepository repo)
    {
        var user = repo.AddUser(new User
        {
            Email = email,
            Password = password
        });

        return user;
    }
}

