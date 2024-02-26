namespace AdSpot.Models;

public class ListingType
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int PlatformId { get; set; }
    public Platform Platform { get; set; }
}
