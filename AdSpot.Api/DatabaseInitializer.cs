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
                new User { Email = "matt", Password = "matt" },
                new User { Email = "akarsh", Password = "akarsh" }
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
                new Connection { UserId = 2, PlatformId = 1, Handle = "matt-fb", Token = "token" },
                new Connection { UserId = 2, PlatformId = 2, Handle = "matt-twitter", Token = "token" },
                new Connection { UserId = 2, PlatformId = 3, Handle = "matt-ig", Token = "token" },

                new Connection { UserId = 3, PlatformId = 1, Handle = "akarsh-fb", Token = "token" },
                new Connection { UserId = 3, PlatformId = 2, Handle = "akarsh-twitter", Token = "token" },
                new Connection { UserId = 3, PlatformId = 3, Handle = "akarsh-ig", Token = "token" }
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
                new Listing { UserId = 2, ListingTypeId = 1, Price = 10 },
                new Listing { UserId = 2, ListingTypeId = 2, Price = 20 },
                new Listing { UserId = 2, ListingTypeId = 3, Price = 30 },
                new Listing { UserId = 2, ListingTypeId = 4, Price = 40 },
                new Listing { UserId = 2, ListingTypeId = 5, Price = 50 },
                new Listing { UserId = 2, ListingTypeId = 6, Price = 60 },

                new Listing { UserId = 3, ListingTypeId = 1, Price = 70 },
                new Listing { UserId = 3, ListingTypeId = 2, Price = 80 },
                new Listing { UserId = 3, ListingTypeId = 3, Price = 90 },
                new Listing { UserId = 3, ListingTypeId = 4, Price = 100 },
                new Listing { UserId = 3, ListingTypeId = 5, Price = 110 },
                new Listing { UserId = 3, ListingTypeId = 6, Price = 120 }
            };
            foreach (var listing in listings)
            {
                listing.PlatformId = dbContext.ListingTypes.Find(listing.ListingTypeId).PlatformId;
            }
            dbContext.Listings.AddRange(listings);
            dbContext.SaveChanges();
        }

        if (dbContext.OrderStatuses.FirstOrDefault() is null)
        {
            var orderStatuses = Enum.GetValues<OrderStatusEnum>()
                .Select(x => new OrderStatus
                {
                    OrderStatusId = x,
                    Name = x.ToString()
                });
            dbContext.OrderStatuses.AddRange(orderStatuses);
            dbContext.SaveChanges();
        }

        if (dbContext.Orders.FirstOrDefault() is null)
        {
            var orders = new List<Order>
            {
                new Order { UserId = 2, ListingId = 7, Description = "Do something", OrderStatusId = OrderStatusEnum.Pending },
                new Order { UserId = 2, ListingId = 8, Description = "Do something", OrderStatusId = OrderStatusEnum.Pending },
                new Order { UserId = 2, ListingId = 9, Description = "Do something", OrderStatusId = OrderStatusEnum.Accepted },
                new Order { UserId = 2, ListingId = 10, Description = "Do something", OrderStatusId = OrderStatusEnum.Rejected },
                new Order { UserId = 2, ListingId = 11, Description = "Do something", OrderStatusId = OrderStatusEnum.Completed, Deliverable = "Link to deliverable" },
                new Order { UserId = 2, ListingId = 12, Description = "Do something", OrderStatusId = OrderStatusEnum.Completed, Deliverable = "Link to deliverable" },

                new Order { UserId = 3, ListingId = 1, Description = "Do something", OrderStatusId = OrderStatusEnum.Pending },
                new Order { UserId = 3, ListingId = 2, Description = "Do something", OrderStatusId = OrderStatusEnum.Pending },
                new Order { UserId = 3, ListingId = 3, Description = "Do something", OrderStatusId = OrderStatusEnum.Accepted },
                new Order { UserId = 3, ListingId = 4, Description = "Do something", OrderStatusId = OrderStatusEnum.Rejected },
                new Order { UserId = 3, ListingId = 5, Description = "Do something", OrderStatusId = OrderStatusEnum.Completed, Deliverable = "Link to deliverable" },
                new Order { UserId = 3, ListingId = 6, Description = "Do something", OrderStatusId = OrderStatusEnum.Completed, Deliverable = "Link to deliverable" }
            };
            foreach (var order in orders)
            {
                var listing = dbContext.Listings.Find(order.ListingId);
                order.Price = listing.Price;
            }
            dbContext.Orders.AddRange(orders);
            dbContext.SaveChanges();
        }

        dbContext.SaveChanges();
    }
}
