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
    public IQueryable<Order> GetPendingRequests(int userId, OrderRepository repo)
    {
        return repo.GetRequestsByStatus(userId, OrderStatusEnum.Pending);
    }

    [UsePaging]
    [UseProjection]
    [UseSorting]
    public IQueryable<Order> GetAcceptedRequests(int userId, OrderRepository repo)
    {
        return repo.GetRequestsByStatus(userId, OrderStatusEnum.Accepted);
    }

    [UsePaging]
    [UseProjection]
    [UseSorting]
    public IQueryable<Order> GetRejectedRequests(int userId, OrderRepository repo)
    {
        return repo.GetRequestsByStatus(userId, OrderStatusEnum.Rejected);
    }

    [UsePaging]
    [UseProjection]
    [UseSorting]
    public IQueryable<Order> GetCompletedRequests(int userId, OrderRepository repo)
    {
        return repo.GetRequestsByStatus(userId, OrderStatusEnum.Completed);
    }

    [UsePaging]
    [UseProjection]
    [UseSorting]
    public IQueryable<Order> GetPendingOrders(int userId, OrderRepository repo)
    {
        return repo.GetOrdersByStatus(userId, OrderStatusEnum.Pending);
    }

    [UsePaging]
    [UseProjection]
    [UseSorting]
    public IQueryable<Order> GetAcceptedOrders(int userId, OrderRepository repo)
    {
        return repo.GetOrdersByStatus(userId, OrderStatusEnum.Accepted);
    }

    [UsePaging]
    [UseProjection]
    [UseSorting]
    public IQueryable<Order> GetRejectedOrders(int userId, OrderRepository repo)
    {
        return repo.GetOrdersByStatus(userId, OrderStatusEnum.Rejected);
    }

    [UsePaging]
    [UseProjection]
    [UseSorting]
    public IQueryable<Order> GetCompletedOrders(int userId, OrderRepository repo)
    {
        return repo.GetOrdersByStatus(userId, OrderStatusEnum.Completed);
    }
}
