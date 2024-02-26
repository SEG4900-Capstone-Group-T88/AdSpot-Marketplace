namespace AdSpot.Models;

public class User
{
    public int UserId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public ICollection<ConnectedAccount> ConnectedAccounts { get; set; }
}
