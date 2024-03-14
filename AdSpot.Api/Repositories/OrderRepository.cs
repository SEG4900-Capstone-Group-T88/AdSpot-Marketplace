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
}
