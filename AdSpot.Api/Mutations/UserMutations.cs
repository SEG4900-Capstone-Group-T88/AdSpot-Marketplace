namespace AdSpot.Api.Mutations;

[MutationType]
public class UserMutations
{
    [Error<AccountWithEmailAlreadyExistsError>]
    public MutationResult<AddUserPayload> AddUser(
        [UseFluentValidation, UseValidator<AddUserInputValidator>] AddUserInput input,
        UserRepository repo,
        [Service] IOptions<JwtOptions> jwtOptions,
        [Service] KeyManager keyManager
    )
    {
        var user = repo.GetUserByEmail(input.Email);
        if (user is not null)
        {
            return new(new AccountWithEmailAlreadyExistsError(input.Email));
        }

        user = repo.AddUser(
            new User
            {
                Email = input.Email,
                Password = input.Password,
                FirstName = input.FirstName,
                LastName = input.LastName
            }
        );

        var token = JwtUtils.GenerateToken(user, jwtOptions, keyManager);

        return new AddUserPayload { User = user, Token = token };
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
    public MutationResult<LoginPayload> Login(
        string email,
        string password,
        UserRepository repo,
        [Service] IOptions<JwtOptions> jwtOptions,
        [Service] KeyManager keyManager
    )
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

        var token = JwtUtils.GenerateToken(user, jwtOptions, keyManager);

        return new LoginPayload { User = user, Token = token };
    }
}
