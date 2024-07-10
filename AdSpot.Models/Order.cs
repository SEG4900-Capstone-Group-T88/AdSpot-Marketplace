namespace AdSpot.Models;

public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public OrderStatusEnum OrderStatusId { get; set; } = OrderStatusEnum.Pending;
    public OrderStatus OrderStatus { get; set; }

    public int ListingId { get; set; }
    public Listing Listing { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public decimal Price { get; set; }
    public string Description { get; set; }

    //public string? Deliverable { get; set; }
    //public float? Rating { get; set; }
}

public enum OrderPov
{
    Buyer,
    Seller
}
