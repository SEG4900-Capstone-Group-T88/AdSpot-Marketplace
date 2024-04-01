namespace AdSpot.Api.Repositories;

public class ListingTypeRepository
{
    private readonly AdSpotDbContext context;

    public ListingTypeRepository(AdSpotDbContext context)
    {
        this.context = context;
    }

    public IQueryable<ListingType> GetAllListingTypes()
    {
        return context.ListingTypes;
    }

    public IQueryable<ListingType> GetListingTypeById(int listingTypeId)
    {
        return context.ListingTypes.Where(x => x.ListingTypeId == listingTypeId);
    }
}
