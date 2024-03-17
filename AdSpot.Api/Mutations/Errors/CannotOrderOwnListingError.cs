namespace AdSpot.Api.Mutations.Errors;

public record CannotOrderOwnListingError
{
    public string Message { get; } = "Cannot order own listing.";
}
