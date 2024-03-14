namespace AdSpot.Api.Queries;

[QueryType]
public class OrderQueries
{
    [UseFiltering]
    public IQueryable<Order> GetOrders(OrderRepository repo)
    {
        return repo.GetAllOrders();
    }

    [UseFirstOrDefault]
    public IQueryable<Order> GetOrderById(int orderId, OrderRepository repo)
    {
        return repo.GetOrderById(orderId);
    }
}
