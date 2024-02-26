namespace AdSpot.Api;

public class AdSpotDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<ConnectedAccount> ConnectedAccounts { get; set; }
    public DbSet<ListingType> ListingTypes { get; set; }
    public DbSet<Platform> Platforms { get; set; }

    public AdSpotDbContext(DbContextOptions<AdSpotDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ConnectedAccount>()
            .HasKey(a => new { a.UserId, a.PlatformId });

        modelBuilder.Entity<User>()
            .HasMany(e => e.ConnectedAccounts)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId);

        modelBuilder.Entity<Platform>()
            .HasMany(e => e.ListingTypes)
            .WithOne(e => e.Platform)
            .HasForeignKey(e => e.PlatformId);
    }
}
