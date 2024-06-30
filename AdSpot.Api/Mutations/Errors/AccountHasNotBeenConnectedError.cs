namespace AdSpot.Api.Mutations.Errors;

public record AccountHasNotBeenConnectedError
{
    public AccountHasNotBeenConnectedError(int userId, int platformId)
    {
        Message = $"User with id `{userId}` has no connected account for platform with id `{platformId}`.";
    }

    public string Message { get; }
}
