namespace AdSpot.Api.Repositories;

public class OrderRepository
{
    private readonly AdSpotDbContext context;

    public OrderRepository(AdSpotDbContext context)
    {
        this.context = context;
    }

    public IQueryable<Order> GetAllOrders()
    {
        return context.Orders;
    }

    public IQueryable<Order> GetOrderById(int orderId)
    {
        return context.Orders.Where(o => o.OrderId == orderId);
    }

    public IQueryable<Order> GetPendingRequests(int userId)
    {
        return context.Orders.Where(o => o.Listing.UserId == userId && o.OrderStatusId == OrderStatusEnum.Pending);
    }

    public IQueryable<Order> GetAllPurchases(int userId)
    {
        return context.Orders.Where(o => o.UserId == userId);
    }
}
