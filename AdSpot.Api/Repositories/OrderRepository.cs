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
        return context.Orders.First(o => o.OrderId == order.OrderId);
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

    public IQueryable<Order> GetOrders(int userId, OrderPov pov)
    {
        return pov switch
        {
            OrderPov.Buyer => context.Orders.Where(o => o.UserId == userId),
            OrderPov.Seller => context.Orders.Where(o => o.Listing.UserId == userId),
        };
    }

    public Order AcceptOrder(int orderId)
    {
        var order = context.Orders.FirstOrDefault(o => o.OrderId == orderId);
        if (order is not null)
        {
            order.OrderStatusId = OrderStatusEnum.Accepted;
            context.SaveChanges();
        }
        return context.Orders.First(o => o.OrderId == orderId);
    }

    public Order RejectOrder(int orderId)
    {
        var order = context.Orders.FirstOrDefault(o => o.OrderId == orderId);
        if (order is not null)
        {
            order.OrderStatusId = OrderStatusEnum.Rejected;
            context.SaveChanges();
        }
        return context.Orders.First(o => o.OrderId == orderId);
    }
}
