namespace AdSpot.Api.Repositories;

public class ListingRepository
{
    private readonly AdSpotDbContext context;

    public ListingRepository(AdSpotDbContext context)
    {
        this.context = context;
    }

    public IQueryable<Listing> GetAllListings()
    {
        return context.Listings;
    }

    public IQueryable<Listing> GetListingById(int listingId)
    {
        return context.Listings.Where(l => l.ListingId == listingId);
    }

    public IQueryable<Listing> AddListing(Listing listing)
    {
        context.Listings.Add(listing);
        context.SaveChanges();
        return context.Listings.Where(x => x.ListingId == listing.ListingId);
    }
}
