using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdSpot.Models;

public class SocialMediaAccount {
    [Key]
    public string AccountPlatform { get; set; }

    public string UserEmail { get; set; }

    public string AccountHandle { get; set; }

    public string APIToken { get; set; }

    [ForeignKey("UserEmail")]
    public virtual User user { get; set; }
}