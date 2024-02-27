namespace AdSpot.Api;

public class AdSpotDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Connection> Connections { get; set; }
    public DbSet<ListingType> ListingTypes { get; set; }
    public DbSet<Platform> Platforms { get; set; }
    public DbSet<Listing> Listings { get; set; }

    public AdSpotDbContext(DbContextOptions<AdSpotDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Connection>()
            .HasKey(a => new { a.UserId, a.PlatformId });

        modelBuilder.Entity<User>()
            .HasMany(e => e.Connections)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId);
        modelBuilder.Entity<User>()
            .HasMany(e => e.Listings)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId);

        modelBuilder.Entity<Platform>()
            .HasMany(e => e.ListingTypes)
            .WithOne(e => e.Platform)
            .HasForeignKey(e => e.PlatformId);

        modelBuilder.Entity<Listing>()
            .HasOne(e => e.ListingType)
            .WithOne()
            .HasForeignKey<Listing>(e => e.ListingTypeId);
    }
}
