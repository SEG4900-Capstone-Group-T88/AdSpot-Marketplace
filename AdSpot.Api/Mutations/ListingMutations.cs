namespace AdSpot.Api.Mutations;

[MutationType]
public class ListingMutations
{
    //Seems like we can't use projections here
    //[UseProjection]
    [Error<InvalidListingTypeIdError>]
    [Error<AccountHasNotBeenConnectedError>]
    public MutationResult<IQueryable<Listing>> AddListing(
        int listingTypeId, int userId, decimal price,
        ConnectionRepository connectionRepo,
        ListingRepository listingRepo,
        ListingTypeRepository listingTypesRepo)
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

        var listing = listingRepo.AddListing(new Listing
        {
            PlatformId = listingType.PlatformId,
            ListingTypeId = listingTypeId,
            UserId = userId,
            Price = price
        });

        return new(listing);
    }

    //Seems like we can't use projections here
    //[UseProjection]
    [Error<InvalidListingIdError>]
    [Error<CannotOrderOwnListingError>]
    [Error<ListingPriceHasChangedError>]
    public MutationResult<IQueryable<Order>> OrderListing(int listingId, int userId, decimal price, string description,
        ListingRepository listingRepo, OrderRepository orderRepo)
    {
        var listing = listingRepo.GetListingById(listingId).FirstOrDefault();
        if (listing is null)
        {
            return new(new InvalidListingIdError(listingId));
        }

        if (userId == listing.UserId)
        {
            return new(new CannotOrderOwnListingError());
        }

        if (listing.Price != price)
        {
            return new(new ListingPriceHasChangedError(price, listing.Price));
        }

        var order = orderRepo.AddOrder(new Order
        {
            ListingId = listingId,
            UserId = userId,
            Price = price,
            Description = description
        });

        return new(order);
    }
}
