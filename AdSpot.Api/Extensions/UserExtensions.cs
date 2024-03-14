namespace AdSpot.Api.Extensions;

[ExtendObjectType<User>]
public class UserExtensions
{
    public IQueryable<Order> GetPendingOrderRequests([Parent] User user, OrderRepository repo)
    {
        return repo.GetPendingOrderRequestsForUser(user.UserId);
    }
}
