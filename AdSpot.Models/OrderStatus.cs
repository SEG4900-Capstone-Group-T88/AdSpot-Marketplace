namespace AdSpot.Models;

public class OrderStatus
{
    public OrderStatusEnum OrderStatusId { get; set; }
    public string Name { get; set; }

    public ICollection<Order> Orders { get; set; }
}

public enum OrderStatusEnum
{
    Pending = 0,
    Accepted = 1,
    Rejected = 2,
    Completed = 3
}
