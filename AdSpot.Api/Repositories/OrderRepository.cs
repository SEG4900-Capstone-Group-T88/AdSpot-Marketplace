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

    public Order AddOrder(Order order)
    {
        context.Orders.Add(order);
        context.SaveChanges();
        return order;
    }

    public IQueryable<Order> GetOrderById(int orderId)
    {
        return context.Orders.Where(o => o.OrderId == orderId);
    }

    public IQueryable<Order> GetPendingRequests(int userId)
    {
        return context.Orders.Where(o => o.Listing.UserId == userId && o.OrderStatusId == OrderStatusEnum.Pending);
    }

    public IQueryable<Order> GetAcceptedRequests(int userId)
    {
        return context.Orders.Where(o => o.Listing.UserId == userId && o.OrderStatusId == OrderStatusEnum.Accepted);
    }

    public IQueryable<Order> GetRejectedRequests(int userId)
    {
        return context.Orders.Where(o => o.Listing.UserId == userId && o.OrderStatusId == OrderStatusEnum.Rejected);
    }

    public IQueryable<Order> GetPendingOrders(int userId)
    {
        return context.Orders.Where(o => o.UserId == userId && o.OrderStatusId == OrderStatusEnum.Pending);
    }

    public IQueryable<Order> GetAcceptedOrders(int userId)
    {
        return context.Orders.Where(o => o.UserId == userId && o.OrderStatusId == OrderStatusEnum.Accepted);
    }

    public IQueryable<Order> GetRejectedOrders(int userId)
    {
        return context.Orders.Where(o => o.UserId == userId && o.OrderStatusId == OrderStatusEnum.Rejected);
    }
}
