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

    [UseProjection]
    [UseSorting]
    public IQueryable<Order> GetPendingRequests(int userId, OrderRepository repo)
    {
        return repo.GetPendingRequests(userId);
    }

    [UseProjection]
    [UseSorting]
    public IQueryable<Order> GetAcceptedRequests(int userId, OrderRepository repo)
    {
        return repo.GetAcceptedRequests(userId);
    }

    [UseProjection]
    [UseSorting]
    public IQueryable<Order> GetRejectedRequests(int userId, OrderRepository repo)
    {
        return repo.GetRejectedRequests(userId);
    }

    [UseProjection]
    [UseSorting]
    public IQueryable<Order> GetPendingOrders(int userId, OrderRepository repo)
    {
        return repo.GetPendingOrders(userId);
    }

    [UseProjection]
    [UseSorting]
    public IQueryable<Order> GetAcceptedOrders(int userId, OrderRepository repo)
    {
        return repo.GetAcceptedOrders(userId);
    }

    [UseProjection]
    [UseSorting]
    public IQueryable<Order> GetRejectedOrders(int userId, OrderRepository repo)
    {
        return repo.GetRejectedOrders(userId);
    }
}
