namespace AdSpot.Api.Subscriptions;

[ExtendObjectType(OperationTypeNames.Subscription)]
public class NewConnectionSubscription
{
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
}
