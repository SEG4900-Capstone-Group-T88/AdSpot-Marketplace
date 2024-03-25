namespace AdSpot.Models;

public class Platform
{
    public int PlatformId { get; set; }
    public string Name { get; set; }
    public ICollection<ListingType> ListingTypes { get; set; }
}
