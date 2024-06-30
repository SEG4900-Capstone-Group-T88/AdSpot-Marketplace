using HotChocolate.Authorization;

namespace AdSpot.Api.Queries;

[QueryType]
public class OrderQueries
{
    [Authorize]
    [UseProjection]
    [UseFiltering]
    public IQueryable<Order> GetOrders(OrderRepository repo)
    {
        return repo.GetAllOrders();
    }

    [Authorize]
    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<Order> GetOrderById(int orderId, OrderRepository repo)
    {
        return repo.GetOrderById(orderId);
    }

    [Authorize]
    [UsePaging]
    [UseProjection]
    [UseSorting]
    public IQueryable<Order> GetOrdersByStatus(
        int userId,
        OrderStatusEnum status,
        OrderRepository repo
    )
    {
        return repo.GetOrdersByStatus(userId, status);
    }

    [Authorize]
    [UsePaging]
    [UseProjection]
    [UseSorting]
    public IQueryable<Order> GetRequestsByStatus(
        int userId,
        OrderStatusEnum status,
        OrderRepository repo
    )
    {
        return repo.GetRequestsByStatus(userId, status);
    }
}
