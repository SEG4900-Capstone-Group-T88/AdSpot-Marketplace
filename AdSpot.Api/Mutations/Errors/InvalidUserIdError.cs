namespace AdSpot.Api.Mutations.Errors;

public record InvalidUserIdError
{
    public InvalidUserIdError(int userId)
    {
        Message = $"User with id `{userId}` does not exist.";
    }

    public string Message { get; }
}
