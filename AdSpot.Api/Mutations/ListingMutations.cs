namespace AdSpot.Api.Mutations;

[MutationType]
public class ListingMutations
{
    [Authorize]
    //Seems like we can't use projections here
    //[UseProjection]
    [Error<InvalidListingTypeIdError>]
    [Error<AccountHasNotBeenConnectedError>]
    public async Task<FieldResult<Listing>> AddListing(
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

    [Authorize]
    [Error<InvalidPriceError>]
    [Error<InvalidListingIdError>]
    [Error<ListingDoesNotBelongToUserError>]
    public FieldResult<Listing> UpdateListingPrice(
        int listingId,
        int userId,
        decimal price,
        ListingRepository listingRepo
    )
    {
        if (price <= 0)
        {
            return new(new InvalidPriceError(price));
        }

        var listing = listingRepo.GetListingById(listingId);
        if (listing is null)
        {
            return new(new InvalidListingIdError(listingId));
        }

        if (listing.UserId != userId)
        {
            return new(new ListingDoesNotBelongToUserError(listingId, userId));
        }

        listing = listingRepo.UpdatePrice(listingId, price);

        return new(listing);
    }
}
