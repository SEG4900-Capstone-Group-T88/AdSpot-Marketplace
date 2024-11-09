namespace AdSpot.Models;

public class Flair
{
    public int UserId { get; set; }
    public User User { get; set; }
    public string FlairTitle { get; set; }
}