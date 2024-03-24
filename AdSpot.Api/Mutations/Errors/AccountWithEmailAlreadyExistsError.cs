namespace AdSpot.Api.Mutations.Errors;

public record AccountWithEmailAlreadyExistsError
{
    public AccountWithEmailAlreadyExistsError(string email)
    {
        Message = $"An account with the email {email} already exists.";
    }

    public string Message { get; }
}
