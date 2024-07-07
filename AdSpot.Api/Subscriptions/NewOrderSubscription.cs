namespace AdSpot.Api.Subscriptions;

[ExtendObjectType(OperationTypeNames.Subscription)]
public class NewOrderSubscription
{
    public ValueTask<ISourceStream<Order>> SubscribeToNewOrders(
        int userId,
        ITopicEventReceiver receiver,
        CancellationToken cancellationToken
    )
    {
        var topicName = $"{userId}_{nameof(OnNewOrder)}";
        return receiver.SubscribeAsync<Order>(topicName, cancellationToken);
    }

    [Subscribe(With = nameof(SubscribeToNewOrders))]
    public Order OnNewOrder([EventMessage] Order order)
    {
        return order;
    }
}
