namespace AdSpot.Api.Mutations.Errors;

public record ListingDoesNotBelongToUserError
{
    public ListingDoesNotBelongToUserError(int listingId, int userId)
    {
        Message = $"Listing with id {listingId} does not belong to user with id {userId}";
    }

    public string Message { get; }
}
