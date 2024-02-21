using System.ComponentModel.DataAnnotations;

namespace AdSpot.Models;

public class User
{
    [Key]
    public string Email { get; set; }
    public string Password { get; set; }

    public virtual ICollection<SocialMediaAccount> ConnectedAccounts { get; set; }
}
