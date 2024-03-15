namespace AdSpot.Models;

public class Connection
{
    public string Handle { get; set; }
    public string Token { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public int PlatformId { get; set; }
    public Platform Platform { get; set; }
}
