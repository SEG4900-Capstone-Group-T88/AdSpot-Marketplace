namespace AdSpot.Api;

public class AdSpotDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<SocialMediaAccount> SocialMediaAccounts { get; set; }

    public AdSpotDbContext(DbContextOptions<AdSpotDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);
    }
}
