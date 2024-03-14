using AdSpot.Api.Mutations.Errors;

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

    [Error<UserNotFoundError>]
    [Error<UserInvalidCredentialsError>]
    public MutationResult<LoginPayload> Login(string email, string password, UserRepository repo)
    {
        var user = repo.GetUserByEmail(email);

        if (user is null)
        {
            return new(new UserNotFoundError(email));
        }

        user = repo.ValidateUser(email, password);
        if (user is null)
        {
            return new(new UserInvalidCredentialsError());
        }

        //var token = JwtUtils.GenerateToken(user);

        return new LoginPayload
        {
            User = user,
            //Token = token
        };
    }
}
