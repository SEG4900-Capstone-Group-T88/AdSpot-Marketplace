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

    public User GetUser(int userId, UserRepository repo)
        {
            var user = repo.GetUser(userId);
            return user;
        }

        public User UpdateUser(int userId, string email, string password, UserRepository repo)
            {
                var user = repo.UpdateUser(new User
                {
                    Id = userId,
                    Email = email,
                    Password = password
                });

                return user;
            }

    public int DeleteUser(int userId, UserRepository repo) {
        var deletedId = repo.DeleteUser(userId);
        return deletedId;
    }
}

