namespace AdSpot.Models;

public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public decimal Price { get; set; }

    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

    // Seller
    public int ListingId { get; set; }
    public Listing Listing { get; set; }

    // Buyer
    public int UserId { get; set; }
    public User User { get; set; }
}
