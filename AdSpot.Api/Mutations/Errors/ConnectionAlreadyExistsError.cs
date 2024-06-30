namespace AdSpot.Api.Mutations.Errors;

public record ConnectionAlreadyExistsError
{
    public ConnectionAlreadyExistsError(int userId, int platformId)
    {
        Message = $"User with id `{userId}` already has a connected account for platform with id `{platformId}`.";
    }

    public string Message { get; }
}
