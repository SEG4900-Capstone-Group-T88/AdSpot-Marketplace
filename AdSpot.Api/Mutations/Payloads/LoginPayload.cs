namespace AdSpot.Api.Mutations.Payloads;

public class LoginPayload
{
    public User? User { get; set; }
    public string? Token { get; set; }
}
