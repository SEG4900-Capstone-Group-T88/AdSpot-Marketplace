namespace AdSpot.Api.Mutations;

[MutationType]
public class UserMutations
{
    [Error<UserNotFoundError>]
    public MutationResult<AddUserPayload> AddUser(string email, string password, string firstName, string lastName, UserRepository repo, [Service] IConfiguration config)
    {
        var user = repo.GetUserByEmail(email);
        if (user is not null)
        {
            return new(new UserNotFoundError(email));
        }

        user = repo.AddUser(new User
        {
            Email = email,
            Password = password,
            FirstName = firstName,
            LastName = lastName
        });

        var token = JwtUtils.GenerateToken(user, config);

        return new AddUserPayload
        {
            User = user,
            Token = token
        };
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
    public MutationResult<LoginPayload> Login(string email, string password, UserRepository repo, [Service] IConfiguration config)
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

        var token = JwtUtils.GenerateToken(user, config);

        return new LoginPayload
        {
            User = user,
            Token = token
        };
    }
}
