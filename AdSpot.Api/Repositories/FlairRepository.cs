namespace AdSpot.Api.Repositories;

public class FlairRepository
{
    private readonly AdSpotDbContext context;

    public FlairRepository(AdSpotDbContext context)
    {
        this.context = context;
    }

    public IQueryable<Flair> GetFlair(int userId, string flairTitle)
    {
        return context.Flairs.Where(x => x.UserId == userId && x.FlairTitle == flairTitle);
    }

    public IQueryable<Flair> GetFlairs(int userId)
    {
        return context.Flairs.Where(x => x.UserId == userId);
    }

    public IQueryable<Flair> AddFlair(Flair flair)
    {
        context.Flairs.Add(flair);
        context.SaveChanges();
        return context.Flairs.Where(x => x.UserId == flair.UserId);
    }

    public IQueryable<Flair> DeleteFlair(Flair flair)
    {
        context.Flairs.Remove(flair);
        context.SaveChanges();
        return context.Flairs.Where(x => x.UserId == flair.UserId);
    }
}