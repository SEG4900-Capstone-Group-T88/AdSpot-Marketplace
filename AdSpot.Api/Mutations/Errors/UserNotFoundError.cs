namespace AdSpot.Api.Mutations.Errors;

public record UserNotFoundError
{
    public UserNotFoundError(string email)
    {
        Message = $"User with email {email} not found";
    }

    public string Message { get; }
}
