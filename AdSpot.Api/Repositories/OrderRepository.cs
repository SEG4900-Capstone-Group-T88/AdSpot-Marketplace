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

    public IQueryable<Order> AddOrder(Order order)
    {
        context.Orders.Add(order);
        context.SaveChanges();
        return context.Orders.Where(o => o.OrderId == order.OrderId);
    }

    public IQueryable<Order> GetOrderById(int orderId)
    {
        return context.Orders.Where(o => o.OrderId == orderId);
    }

    public IQueryable<Order> GetOrdersByStatus(int userId, OrderStatusEnum status)
    {
        return context.Orders.Where(o => o.UserId == userId && o.OrderStatusId == status);
    }

    public IQueryable<Order> GetRequestsByStatus(int userId, OrderStatusEnum status)
    {
        return context.Orders.Where(o => o.Listing.UserId == userId && o.OrderStatusId == status);
    }
}
