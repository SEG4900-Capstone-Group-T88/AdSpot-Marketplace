namespace AdSpot.Api;

public static class DatabaseInitializer
{
    public static void SeedDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<AdSpotDbContext>();

        dbContext.Database.EnsureCreated();

        if (dbContext.Users.FirstOrDefault() is null)
        {
            var users = new List<User>
            {
                new User { Email = "admin", Password = "admin" },
                new User { Email = "matt", Password = "matt" }
            };
            dbContext.Users.AddRange(users);
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

        if (dbContext.Connections.FirstOrDefault() is null)
        {
            var connections = new List<Connection>
            {
                new Connection { UserId = 2, PlatformId = 1, Handle = "matt-fb", Token = "fb-token" },
                new Connection { UserId = 2, PlatformId = 2, Handle = "matt-twitter", Token = "twitter-token" },
                new Connection { UserId = 2, PlatformId = 3, Handle = "matt-ig", Token = "ig-token" },
            };
            dbContext.Connections.AddRange(connections);
            dbContext.SaveChanges();
        }

        if (dbContext.ListingTypes.FirstOrDefault() is null)
        {
            var listingTypes = new List<ListingType>
            {
                new ListingType { Name = "Post", PlatformId = 1 },
                new ListingType { Name = "Share", PlatformId = 1 },

                new ListingType { Name = "Tweet", PlatformId = 2 },
                new ListingType { Name = "Reweet", PlatformId = 2 },

                new ListingType { Name = "Story", PlatformId = 3 },
                new ListingType { Name = "Post", PlatformId = 3 }
            };
            dbContext.ListingTypes.AddRange(listingTypes);
            dbContext.SaveChanges();
        }

        if (dbContext.Listings.FirstOrDefault() is null)
        {
            var listings = new List<Listing>
            {
                new Listing { UserId = 2, ListingTypeId = 1, Price = 9.99M },
                new Listing { UserId = 2, ListingTypeId = 2, Price = 9.99M },
                new Listing { UserId = 2, ListingTypeId = 3, Price = 9.99M },
                new Listing { UserId = 2, ListingTypeId = 4, Price = 9.99M },
                new Listing { UserId = 2, ListingTypeId = 5, Price = 9.99M },
                new Listing { UserId = 2, ListingTypeId = 6, Price = 9.99M }
            };
            dbContext.Listings.AddRange(listings);
            dbContext.SaveChanges();
        }

        dbContext.SaveChanges();
    }
}
