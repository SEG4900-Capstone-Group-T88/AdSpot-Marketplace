namespace AdSpot.Models;
public class Listing
{
    public int ListingId { get; set; }

    public int ListingTypeId { get; set; }
    public ListingType ListingType { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public decimal Price { get; set; }
}
