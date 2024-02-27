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

    public Listing AddListing(Listing listing)
    {
        context.Listings.Add(listing);
        context.SaveChanges();
        return listing;
    }
}
