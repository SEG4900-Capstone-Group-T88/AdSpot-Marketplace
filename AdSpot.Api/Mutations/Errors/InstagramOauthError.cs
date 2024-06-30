namespace AdSpot.Api.Mutations.Errors;

public record InstagramOauthError
{
    public InstagramOauthError(JObject obj)
    {
        var msg = obj["error_message"]?.ToString();
        if (msg is not null)
        {
            Message = msg;
        }
    }

    public string Message { get; } = "Unable to exchange Instagram auth code for token.";
}
