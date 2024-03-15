namespace AdSpot.Api.Extensions;

[ExtendObjectType<User>]
public class UserExtensions
{
    [UseProjection]
    public IQueryable<Order> GetPendingRequests([Parent] User user, OrderRepository repo)
    {
        return repo.GetPendingRequests(user.UserId);
    }

    [UseProjection]
    [UseFiltering]
    public IQueryable<Order> GetPurchases([Parent] User user, OrderRepository repo)
    {
        return repo.GetAllPurchases(user.UserId);
    }
}
