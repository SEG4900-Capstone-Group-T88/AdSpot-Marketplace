namespace AdSpot.Api.Subscriptions;

[ExtendObjectType(OperationTypeNames.Subscription)]
public class NewListingSubscription
{
    public ValueTask<ISourceStream<Listing>> SubscribeToNewListings(
        int userId,
        ITopicEventReceiver receiver,
        CancellationToken cancellationToken
    )
    {
        var topicName = $"{userId}_{nameof(OnNewListing)}";
        return receiver.SubscribeAsync<Listing>(topicName, cancellationToken);
    }

    [Subscribe(With = nameof(SubscribeToNewListings))]
    public Listing OnNewListing([EventMessage] Listing listing)
    {
        return listing;
    }
}
