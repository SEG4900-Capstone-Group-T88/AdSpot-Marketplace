namespace AdSpot.Api.Mutations;

[MutationType]
public class ListingMutations
{
    [Authorize]
    //Seems like we can't use projections here
    //[UseProjection]
    [Error<InvalidListingTypeIdError>]
    [Error<AccountHasNotBeenConnectedError>]
    public async Task<MutationResult<Listing>> AddListing(
        int listingTypeId,
        int userId,
        decimal price,
        ConnectionRepository connectionRepo,
        ListingRepository listingRepo,
        ListingTypeRepository listingTypesRepo,
        [Service] ITopicEventSender topicEventSender
    )
    {
        var listingType = listingTypesRepo.GetListingTypeById(listingTypeId).FirstOrDefault();
        if (listingType is null)
        {
            return new(new InvalidListingTypeIdError(listingTypeId));
        }

        var connection = connectionRepo.GetConnection(userId, listingType.PlatformId).FirstOrDefault();
        if (connection is null)
        {
            return new(new AccountHasNotBeenConnectedError(userId, listingType.PlatformId));
        }

        var listing = listingRepo.AddListing(
            new Listing
            {
                PlatformId = listingType.PlatformId,
                ListingTypeId = listingTypeId,
                UserId = userId,
                Price = price
            }
        );

        var topicName = $"{userId}_{nameof(NewListingSubscription.OnNewListing)}";
        await topicEventSender.SendAsync(topicName, listing);

        return new(listing);
    }
}
