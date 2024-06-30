namespace AdSpot.Api;

public class AdSpotDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Connection> Connections { get; set; }
    public DbSet<ListingType> ListingTypes { get; set; }
    public DbSet<Platform> Platforms { get; set; }
    public DbSet<Listing> Listings { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set; }

    public AdSpotDbContext(DbContextOptions<AdSpotDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Connection>().HasKey(e => new { e.UserId, e.PlatformId });

        modelBuilder.Entity<User>().HasAlternateKey(e => e.Email);
        modelBuilder.Entity<User>().HasMany(e => e.Connections).WithOne(e => e.User).HasForeignKey(e => e.UserId);
        modelBuilder.Entity<User>().HasMany(e => e.Listings).WithOne(e => e.User).HasForeignKey(e => e.UserId);

        modelBuilder
            .Entity<Platform>()
            .HasMany(e => e.ListingTypes)
            .WithOne(e => e.Platform)
            .HasForeignKey(e => e.PlatformId);

        modelBuilder.Entity<Listing>().HasKey(e => e.ListingId);
        modelBuilder.Entity<Listing>().HasAlternateKey(e => new { e.UserId, e.ListingTypeId });
        modelBuilder
            .Entity<Listing>()
            .HasOne(e => e.ListingType)
            .WithMany(e => e.Listings)
            .HasForeignKey(e => e.ListingTypeId);
        modelBuilder.Entity<Listing>().HasMany(e => e.Orders).WithOne(e => e.Listing).HasForeignKey(e => e.ListingId);
        modelBuilder
            .Entity<Listing>()
            .HasOne(e => e.Connection)
            .WithMany()
            .HasForeignKey(e => new { e.UserId, e.PlatformId });

        modelBuilder.Entity<Order>().HasOne(e => e.User).WithMany(e => e.Orders).HasForeignKey(e => e.UserId);
        modelBuilder
            .Entity<Order>()
            .HasOne(e => e.OrderStatus)
            .WithMany(e => e.Orders)
            .HasForeignKey(e => e.OrderStatusId);

        modelBuilder.Entity<OrderStatus>().Property(e => e.OrderStatusId).ValueGeneratedNever();
    }
}
