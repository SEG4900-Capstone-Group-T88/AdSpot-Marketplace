namespace AdSpot.Models;

public class ConnectedAccount {
    public string Platform { get; set; }
    public string Handle { get; set; }
    public string Token { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}