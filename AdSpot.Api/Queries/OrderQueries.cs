namespace AdSpot.Api.Queries;

[QueryType]
public class OrderQueries
{
    [UseProjection]
    [UseFiltering]
    public IQueryable<Order> GetOrders(OrderRepository repo)
    {
        return repo.GetAllOrders();
    }

    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<Order> GetOrderById(int orderId, OrderRepository repo)
    {
        return repo.GetOrderById(orderId);
    }

    [UsePaging]
    [UseProjection]
    [UseSorting]
    public IQueryable<Order> GetOrdersByStatus(int userId, OrderStatusEnum status, OrderRepository repo)
    {
        return repo.GetOrdersByStatus(userId, status);
    }

    [UsePaging]
    [UseProjection]
    [UseSorting]
    public IQueryable<Order> GetRequestsByStatus(int userId, OrderStatusEnum status, OrderRepository repo)
    {
        return repo.GetRequestsByStatus(userId, status);
    }
}
