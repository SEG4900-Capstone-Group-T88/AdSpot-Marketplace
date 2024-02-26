namespace AdSpot.Api;

public static class DatabaseInitializer
{
    public static void SeedDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<AdSpotDbContext>();

        dbContext.Database.EnsureCreated();

        var numAuthors = 10;
        var numBooks = 100;

        if (dbContext.Authors.FirstOrDefault() is null)
        {
            for (int i = 1; i <= numAuthors; i++)
            {
                var author = new Author { Name = $"Author {i}" };
                dbContext.Authors.Add(author);
            }
            dbContext.SaveChanges();
        }

        if (dbContext.Books.FirstOrDefault() is null)
        {
            for (int i = 1; i <= numBooks; i++)
            {
                var book = new Book { Title = $"Book {i}", AuthorId = i % numAuthors + 1 };
                dbContext.Books.Add(book);
            }
            dbContext.SaveChanges();
        }

        if (dbContext.Users.FirstOrDefault() is null)
        {
            var admin = new User
            {
                Email = "admin",
                Password = "admin"
            };
            dbContext.Users.Add(admin);
            dbContext.SaveChanges();
        }

        if (dbContext.Platforms.FirstOrDefault() is null)
        {
            var platforms = new List<Platform>
            {
                new Platform { Name = "Facebook" },
                new Platform { Name = "Twitter" },
                new Platform { Name = "Instagram" }
            };
            dbContext.Platforms.AddRange(platforms);
            dbContext.SaveChanges();
        }

        if (dbContext.ListingTypes.FirstOrDefault() is null)
        {
            var listingTypes = new List<ListingType>
            {
                new ListingType { Name = "Post", PlatformId = 1 },
                new ListingType { Name = "Tweet", PlatformId = 2 },
                new ListingType { Name = "Story", PlatformId = 3 }
            };
            dbContext.ListingTypes.AddRange(listingTypes);
            dbContext.SaveChanges();
        }

        dbContext.SaveChanges();
    }
}
