namespace AdSpot.Api.Mutations.Errors;

public record ListingPriceHasChangedError
{
    public ListingPriceHasChangedError(decimal oldPrice, decimal newPrice)
    {
        Message = $"The listing price has changed from `{oldPrice}` to `{newPrice}`.";
    }

    public string Message { get; }
}
