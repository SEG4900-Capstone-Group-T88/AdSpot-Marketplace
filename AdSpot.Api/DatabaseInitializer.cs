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
        }

        if (dbContext.Books.FirstOrDefault() is null)
        {
            for (int i = 1; i <= numBooks; i++)
            {
                var book = new Book { Title = $"Book {i}", AuthorId = i % numAuthors + 1 };
                dbContext.Books.Add(book);
            }
        }

        dbContext.SaveChanges();
    }
}
