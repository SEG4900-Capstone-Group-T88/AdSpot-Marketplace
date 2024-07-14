namespace AdSpot.Api.Mutations.Errors;

public record InvalidPriceError
{
    public InvalidPriceError(decimal price)
    {
        Message = $"Price `{price}` is invalid. Must be greater than 0.";
    }

    public string Message { get; }
}
