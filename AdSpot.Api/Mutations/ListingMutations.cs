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
}
