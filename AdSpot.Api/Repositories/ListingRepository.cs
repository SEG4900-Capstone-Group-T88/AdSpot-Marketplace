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

    public Listing? GetListingById(int listingId)
    {
        return context.Listings.FirstOrDefault(l => l.ListingId == listingId);
    }

    public Listing AddListing(Listing listing)
    {
        context.Listings.Add(listing);
        context.SaveChanges();
        return context.Listings.First(x => x.ListingId == listing.ListingId);
    }

    public Listing UpdatePrice(int listingId, decimal price)
    {
        var listing = context.Listings.First(u => u.ListingId == listingId);
        listing.Price = price;
        context.SaveChanges();
        return listing;
    }
}
