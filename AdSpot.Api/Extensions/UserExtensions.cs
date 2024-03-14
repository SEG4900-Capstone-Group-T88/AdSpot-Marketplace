namespace AdSpot.Api.Extensions;

[ExtendObjectType<User>]
public class UserExtensions
{
    public IQueryable<Order> GetSales([Parent] User user, OrderRepository repo)
    {
    }
}
