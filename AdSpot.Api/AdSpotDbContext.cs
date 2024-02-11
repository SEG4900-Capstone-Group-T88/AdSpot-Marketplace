namespace AdSpot.Api;

public class AdSpotDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    public AdSpotDbContext(DbContextOptions<AdSpotDbContext> options) : base(options) { }
}
