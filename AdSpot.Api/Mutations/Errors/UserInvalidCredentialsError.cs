namespace AdSpot.Api.Mutations.Errors;

public record UserInvalidCredentialsError
{
    public string Message { get; } = "Invalid credentials";
}
