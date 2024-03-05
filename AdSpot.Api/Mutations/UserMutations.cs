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

    public User DeleteUser(int userId, UserRepository repo)
    {
        var user = repo.DeleteUser(userId);
        return user;
    }

    public User UpdatePassword(int userId, string password, UserRepository repo)
    {
        var user = repo.UpdatePassword(userId, password);
        return user;
    }
}

