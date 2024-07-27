namespace AdSpot.Api;

public static class DatabaseInitializer
{
    public static void SeedDatabase(this AdSpotDbContext dbContext)
    {
        dbContext.Database.EnsureCreated();

        var random = new Random(0);
        var numUsers = 100;

        if (dbContext.Users.FirstOrDefault() is null)
        {
            var users = new List<User>();

            for (var i = 1; i <= numUsers; i++)
            {
                users.Add(
                    new User
                    {
                        Email = $"user{i}",
                        Password = $"user{i}",
                        FirstName = $"User",
                        LastName = $"{i}"
                    }
                );
            }
            users.AddRange(
                new List<User>
                {
                    new User
                    {
                        Email = "matt",
                        Password = "matt",
                        FirstName = "Matthew",
                        LastName = "Sia"
                    },
                    new User
                    {
                        Email = "akarsh",
                        Password = "akarsh",
                        FirstName = "Akarsh",
                        LastName = "Gharge"
                    },
                    new User
                    {
                        Email = "demian",
                        Password = "demian",
                        FirstName = "Demian",
                        LastName = "Oportus"
                    }
                }
            );

            dbContext.Users.AddRange(users);
            dbContext.SaveChanges();
        }

        var platforms = new List<Platform>
        {
            new Platform { Name = "Facebook" },
            new Platform { Name = "Twitter" },
            new Platform { Name = "Instagram" },
            new Platform { Name = "Youtube" }
        };

        if (dbContext.Platforms.FirstOrDefault() is null)
        {
            dbContext.Platforms.AddRange(platforms);
            dbContext.SaveChanges();
        }

        if (dbContext.Connections.FirstOrDefault() is null)
        {
            var connections = new List<Connection>();
            dbContext
                .Users.ToList()
                .ForEach(user =>
                {
                    dbContext
                        .Platforms.ToList()
                        .ForEach(platform =>
                        {
                            connections.Add(
                                new Connection
                                {
                                    UserId = user.UserId,
                                    PlatformId = platform.PlatformId,
                                    Handle = $"user{user.UserId}-{platform.Name.ToLower()}",
                                    Token = "token"
                                }
                            );
                        });
                });

            dbContext.Connections.AddRange(connections);
            dbContext.SaveChanges();
        }

        var listingTypes = new List<ListingType>
        {
            new ListingType { Name = "Post", PlatformId = 1 },
            new ListingType { Name = "Share", PlatformId = 1 },
            new ListingType { Name = "Tweet", PlatformId = 2 },
            new ListingType { Name = "Retweet", PlatformId = 2 },
            new ListingType { Name = "Story", PlatformId = 3 },
            new ListingType { Name = "Post", PlatformId = 3 },
            new ListingType { Name = "Video", PlatformId = 4 },
            new ListingType { Name = "Stream", PlatformId = 4 }
        };

        if (dbContext.ListingTypes.FirstOrDefault() is null)
        {
            dbContext.ListingTypes.AddRange(listingTypes);
            dbContext.SaveChanges();
        }

        if (dbContext.Listings.FirstOrDefault() is null)
        {
            var minPrice = 1;
            var maxPrice = 1000;

            var listings = new List<Listing>();
            dbContext
                .Users.ToList()
                .ForEach(user =>
                {
                    dbContext
                        .Platforms.ToList()
                        .ForEach(platform =>
                        {
                            dbContext
                                .ListingTypes.Where(x => x.PlatformId == platform.PlatformId)
                                .ToList()
                                .ForEach(listingType =>
                                {
                                    var probability = random.NextDouble();
                                    if (probability < 0.5)
                                    {
                                        var price = (decimal)
                                            Math.Round(random.NextDouble() * (maxPrice - minPrice) + minPrice, 2);
                                        listings.Add(
                                            new Listing
                                            {
                                                UserId = user.UserId,
                                                ListingTypeId = listingType.ListingTypeId,
                                                PlatformId = listingType.PlatformId,
                                                Price = price
                                            }
                                        );
                                    }
                                });
                        });
                });

            dbContext.Listings.AddRange(listings);
            dbContext.SaveChanges();
        }

        if (dbContext.OrderStatuses.FirstOrDefault() is null)
        {
            var orderStatuses = Enum.GetValues<OrderStatusEnum>()
                .Select(x => new OrderStatus { OrderStatusId = x, Name = x.ToString() });
            dbContext.OrderStatuses.AddRange(orderStatuses);
            dbContext.SaveChanges();
        }

        if (dbContext.Orders.FirstOrDefault() is null)
        {
            var numStatus = Enum.GetValues(typeof(OrderStatusEnum)).Length;
            var orders = new List<Order>();
            dbContext
                .Users.ToList()
                .ForEach(user =>
                {
                    dbContext
                        .Listings.ToList()
                        .ForEach(listing =>
                        {
                            var probability = random.NextDouble();
                            if (probability < 0.1 && listing.UserId != user.UserId)
                            {
                                var status = (OrderStatusEnum)random.Next(numStatus);
                                var deliverable = status == OrderStatusEnum.Completed ? "https://www.google.com" : null;
                                orders.Add(
                                    new Order
                                    {
                                        UserId = user.UserId,
                                        ListingId = listing.ListingId,
                                        Description =
                                            @"Hey, everyone! Today, I'm super excited to talk to you about something that's been a game-changer for me as an influencer, and I think it's going to be huge for you too!

It's called AdSpot! 🌟 Now, if you're anything like me, you're always looking for new and innovative ways to connect with brands that align with your values and your audience's interests. And let me tell you, AdSpot is the answer to that!

AdSpot is this incredible online platform that brings influencers like us together with advertisers in a seamless and intuitive way. Whether you're into fashion, beauty, fitness, or whatever your niche may be, AdSpot has got you covered!

One of the things I love most about AdSpot is how easy it is to use. With advanced search filters, you can quickly find campaigns that match your style and preferences. And the best part? You get to set your own rates and terms, giving you full control over your collaborations!

But wait, there's more! AdSpot also offers a secure payment system, ensuring that you get compensated fairly and on time for your hard work. Plus, their dedicated support team is always there to assist you every step of the way.

So, if you're ready to take your influencer game to the next level and connect with amazing brands, then head over to AdSpot today! I've left the link in the description below so you can check it out for yourself. Trust me, you won't regret it!

Thanks for watching, guys! And remember, the opportunities are endless with AdSpot. Let's make some magic happen together! 💫",
                                        OrderStatusId = status,
                                        Deliverable = deliverable
                                    }
                                );
                            }
                        });
                });

            foreach (var order in orders)
            {
                var listing = dbContext.Listings.Find(order.ListingId);
                order.Price = listing!.Price;
            }
            dbContext.Orders.AddRange(orders);
            dbContext.SaveChanges();
        }

        dbContext.SaveChanges();
    }

    public static void SeedDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<AdSpotDbContext>();

        dbContext.SeedDatabase();
    }
}
