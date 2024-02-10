namespace AdSpot.Api;

public class AdSpotDbContext : DbContext
{
    public AdSpotDbContext(DbContextOptions<AdSpotDbContext> options) : base(options)
    {
        if (Authors.Any() == false || Books.Any() == false)
        {
            DummyData();
        }
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    public void DummyData()
    {
        var numAuthors = 10;
        for (int i = 1; i <= numAuthors; i++)
        {
            var author = new Author { Name = $"Author {i}" };
            Authors.Add(author);
        }

        var numBooks = 100;
        for (int i = 1; i <= numBooks; i++)
        {
            var book = new Book { Title = $"Book {i}", AuthorId = i % numAuthors + 1 };
            Books.Add(book);
        }

        SaveChanges();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasMany(a => a.Books)
            .WithOne(b => b.Author)
            .HasForeignKey(b => b.AuthorId);
        });
    }
}
