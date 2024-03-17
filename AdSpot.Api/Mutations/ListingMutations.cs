namespace AdSpot.Api.Mutations;

[MutationType]
public class ListingMutations
{
    [UseProjection]
    public IQueryable<Listing> AddListing(int platformId, int listingTypeId, int userId, decimal price, ListingRepository repo)
    {
        var listing = repo.AddListing(new Listing
        {
            PlatformId = platformId,
            ListingTypeId = listingTypeId,
            UserId = userId,
            Price = price
        });

        return listing;
    }

    // Seems like we can't use projections here
    [Error<InvalidListingIdError>]
    [Error<ListingPriceHasChangedError>]
    public MutationResult<Order> OrderListing(int listingId, int userId, decimal price, string description,
        ListingRepository listingRepo, OrderRepository orderRepo)
    {
        var listing = listingRepo.GetListingById(listingId).FirstOrDefault();
        if (listing is null)
        {
            return new(new InvalidListingIdError(listingId));
        }

        if (listing.Price != price)
        {
            return new(new ListingPriceHasChangedError(price, listing.Price));
        }

        var order = new Order
        {
            ListingId = listingId,
            UserId = userId,
            Price = price,
            Description = description
        };
        return orderRepo.AddOrder(order);
    }
}
