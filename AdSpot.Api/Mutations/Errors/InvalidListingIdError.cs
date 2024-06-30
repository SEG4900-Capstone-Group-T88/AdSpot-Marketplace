namespace AdSpot.Api.Mutations.Errors;

public record InvalidListingIdError
{
    public InvalidListingIdError(int listingId)
    {
        Message = $"Listing with id `{listingId}` does not exist.";
    }

    public string Message { get; }
}
