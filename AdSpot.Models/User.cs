using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AdSpot.Models;

public class User : IdentityUser
{
    [Key]
    public string Email { get; set; }
    public string Password { get; set; }

    public virtual ICollection<SocialMediaAccount> ConnectedAccounts { get; set; }
}
