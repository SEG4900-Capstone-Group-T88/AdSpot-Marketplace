namespace AdSpot.Api.Mutations.Errors;

public class InvalidListingTypeIdError
{
    public InvalidListingTypeIdError(int listingTypeId)
    {
        Message = $"Listing type with id `{listingTypeId}` does not exist.";
    }

    public string Message { get; }
}
