namespace AdSpot.Api.Mutations.Errors;

public class InvalidOrderIdError
{
    public InvalidOrderIdError(int orderId)
    {
        Message = $"Order with id {orderId} does not exist.";
    }

    public string Message { get; }
}
