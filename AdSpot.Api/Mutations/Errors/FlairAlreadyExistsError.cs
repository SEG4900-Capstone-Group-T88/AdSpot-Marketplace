namespace AdSpot.Api.Mutations.Errors;

public record FlairAlreadyExistsError
{
    public FlairAlreadyExistsError(string flair)
    {
        Message = $"This account aleadry has a '{flair}' flair added";
    }

    public string Message { get; }
}