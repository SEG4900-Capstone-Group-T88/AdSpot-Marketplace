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

    public IQueryable<Listing> AddListing(Listing listing)
    {
        context.Listings.Add(listing);
        var id = context.SaveChanges();
        return context.Listings.Where(x => x.ListingId == id);
    }
}
