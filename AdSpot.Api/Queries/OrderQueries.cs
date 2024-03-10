namespace AdSpot.Api.Queries;

[QueryType]
public class OrderQueries
{
    [UseFiltering]
    public IQueryable<Order> GetOrders(OrderRepository repo)
    {
        return repo.GetAllOrders();
    }
}
