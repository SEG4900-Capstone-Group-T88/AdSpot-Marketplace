using HotChocolate.Execution;

namespace AdSpot.Api;

[SubscriptionType]
public class Subscription
{
    //https://chillicream.com/docs/hotchocolate/v13/migrating/migrate-from-12-to-13/#subscribeandresolve

    public ValueTask<ISourceStream<Connection>> SubscribeToConnectedAccounts(
        int userId,
        ITopicEventReceiver receiver,
        CancellationToken cancellationToken
    )
    {
        var topicName = $"{userId}_{nameof(OnAccountConnected)}";
        return receiver.SubscribeAsync<Connection>(topicName, cancellationToken);
    }

    [Subscribe(With = nameof(SubscribeToConnectedAccounts))]
    public Connection OnAccountConnected([EventMessage] Connection connection)
    {
        return connection;
    }

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
