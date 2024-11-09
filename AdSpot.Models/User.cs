namespace AdSpot.Models;

public class User
{
    public int UserId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public ICollection<Connection> Connections { get; set; }
    public ICollection<Listing> Listings { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<Flair> Flairs { get; set; }
}
