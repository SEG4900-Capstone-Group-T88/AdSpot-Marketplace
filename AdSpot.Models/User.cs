using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AdSpot.Models;

public class User : IdentityUser
{
    public int UserId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public ICollection<Connection> Connections { get; set; }
    public ICollection<Listing> Listings { get; set; }
    public ICollection<Order> Orders { get; set; }
}
