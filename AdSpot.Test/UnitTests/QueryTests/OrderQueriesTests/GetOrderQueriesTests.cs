using AdSpot.Models;

namespace AdSpot.Test.UnitTests.OrderQueriesTests;

[Collection("adspot-inmemory-db")]
public class GetOrderQueriesTests
{
    public const string GetOrderByIdQuery = """
            query GetOrderById($orderId: Int!) {
                orderById(orderId: $orderId) {
                    userId
                    listingId
                    price
                    description
                }
            }
        """;

    public const string GetOrdersQuery = """
            query GetOrders($userId: Int!, $pov: OrderPov!) {
                orders(userId: $userId, pov: $pov) {
                    totalCount
                }
            }
        """;

    public const string GetOrdersByStatusQuery = """
            query GetOrdersByStatus($userId: Int!, $status: OrderStatusEnum!) {
                ordersByStatus(userId: $userId, status: $status) {
                    totalCount
                }
            }
        """;

    public const string GetRequestsByStatusQuery = """
            query GetRequestsByStatus($userId: Int!, $status: OrderStatusEnum!) {
                requestsByStatus(userId: $userId, status: $status) {
                    totalCount
                }
            }
        """;

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetOrderByOrderIdSuccessful()
    {
        var result = await TestServices.ExecuteRequestAsync(
            scope =>
            {
                var context = scope.ServiceProvider.GetRequiredService<AdSpotDbContext>();
                context.Connections.Add(
                    new Connection
                    {
                        UserId = TestDatabase.TestUser.UserId,
                        PlatformId = TestDatabase.Platforms.First().PlatformId,
                        Handle = "TestAccountHandle",
                        Token = "TestApiToken"
                    }
                );
                context.SaveChanges();

                context.Listings.Add(
                    new Listing
                    {
                        ListingId = 1,
                        PlatformId = TestDatabase.Platforms.First().PlatformId,
                        UserId = TestDatabase.TestUser.UserId,
                        ListingTypeId = TestDatabase
                            .ListingTypes.First(listingType =>
                                listingType.PlatformId == TestDatabase.Platforms.First().PlatformId
                            )
                            .PlatformId
                    }
                );
                context.SaveChanges();

                context.Orders.Add(
                    new Order
                    {
                        OrderId = 1,
                        ListingId = 1,
                        UserId = TestDatabase.TestUser.UserId,
                        Price = 100.00M,
                        Description = "Test Order"
                    }
                );
                context.SaveChanges();
            },
            b =>
                b.SetDocument(GetOrderByIdQuery)
                    .SetVariableValues(new Dictionary<string, object?> { { "orderId", 1 } }.AsReadOnly())
        );

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetOrderByOrderIdNoID()
    {
        var result = await TestServices.ExecuteRequestAsync(b =>
            b.SetDocument(GetOrderByIdQuery)
                .SetVariableValues(new Dictionary<string, object?> { { "orderId", 1 } }.AsReadOnly())
        );

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetOrdersAsBuyer()
    {
        var result = await TestServices.ExecuteRequestAsync(
            scope =>
            {
                var context = scope.ServiceProvider.GetRequiredService<AdSpotDbContext>();
                context.Connections.Add(
                    new Connection
                    {
                        UserId = TestDatabase.TestUser.UserId,
                        PlatformId = TestDatabase.Platforms.First().PlatformId,
                        Handle = "TestAccountHandle",
                        Token = "TestApiToken"
                    }
                );
                context.SaveChanges();

                context.Listings.Add(
                    new Listing
                    {
                        ListingId = 1,
                        PlatformId = TestDatabase.Platforms.First().PlatformId,
                        UserId = TestDatabase.TestUser.UserId,
                        ListingTypeId = TestDatabase
                            .ListingTypes.First(listingType =>
                                listingType.PlatformId == TestDatabase.Platforms.First().PlatformId
                            )
                            .PlatformId
                    }
                );
                context.SaveChanges();

                context.Orders.Add(
                    new Order
                    {
                        OrderId = 1,
                        ListingId = 1,
                        UserId = TestDatabase.TestUser.UserId,
                        Price = 100.00M,
                        Description = "Test Order"
                    }
                );
                context.SaveChanges();
            },
            b =>
                b.SetDocument(GetOrdersQuery)
                    .SetVariableValues(
                        new Dictionary<string, object?>
                        {
                            { "userId", TestDatabase.TestUser.UserId },
                            { "pov", "BUYER" }
                        }.AsReadOnly()
                    )
        );

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetOrdersAsSeller()
    {
        var result = await TestServices.ExecuteRequestAsync(
            scope =>
            {
                var context = scope.ServiceProvider.GetRequiredService<AdSpotDbContext>();
                context.Connections.Add(
                    new Connection
                    {
                        UserId = TestDatabase.TestUser.UserId,
                        PlatformId = TestDatabase.Platforms.First().PlatformId,
                        Handle = "TestAccountHandle",
                        Token = "TestApiToken"
                    }
                );
                context.SaveChanges();

                context.Listings.Add(
                    new Listing
                    {
                        ListingId = 1,
                        PlatformId = TestDatabase.Platforms.First().PlatformId,
                        UserId = TestDatabase.TestUser.UserId,
                        ListingTypeId = TestDatabase
                            .ListingTypes.First(listingType =>
                                listingType.PlatformId == TestDatabase.Platforms.First().PlatformId
                            )
                            .PlatformId
                    }
                );
                context.SaveChanges();

                context.Orders.Add(
                    new Order
                    {
                        OrderId = 1,
                        ListingId = 1,
                        UserId = TestDatabase.TestUser.UserId,
                        Price = 100.00M,
                        Description = "Test Order"
                    }
                );
                context.SaveChanges();
            },
            b =>
                b.SetDocument(GetOrdersQuery)
                    .SetVariableValues(
                        new Dictionary<string, object?>
                        {
                            { "userId", TestDatabase.TestUser.UserId },
                            { "pov", "SELLER" }
                        }.AsReadOnly()
                    )
        );

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetOrdersByStatusPending()
    {
        var result = await TestServices.ExecuteRequestAsync(
            scope =>
            {
                var context = scope.ServiceProvider.GetRequiredService<AdSpotDbContext>();
                context.Connections.Add(
                    new Connection
                    {
                        UserId = TestDatabase.TestUser.UserId,
                        PlatformId = TestDatabase.Platforms.First().PlatformId,
                        Handle = "TestAccountHandle",
                        Token = "TestApiToken"
                    }
                );
                context.SaveChanges();

                context.Listings.Add(
                    new Listing
                    {
                        ListingId = 1,
                        PlatformId = TestDatabase.Platforms.First().PlatformId,
                        UserId = TestDatabase.TestUser.UserId,
                        ListingTypeId = TestDatabase
                            .ListingTypes.First(listingType =>
                                listingType.PlatformId == TestDatabase.Platforms.First().PlatformId
                            )
                            .PlatformId
                    }
                );
                context.SaveChanges();

                context.Orders.Add(
                    new Order
                    {
                        OrderId = 1,
                        ListingId = 1,
                        UserId = TestDatabase.TestUser.UserId,
                        Price = 100.00M,
                        Description = "Test Order"
                    }
                );
                context.SaveChanges();
            },
            b =>
                b.SetDocument(GetOrdersByStatusQuery)
                    .SetVariableValues(
                        new Dictionary<string, object?>
                        {
                            { "userId", TestDatabase.TestUser.UserId },
                            { "status", "PENDING" }
                        }.AsReadOnly()
                    )
        );

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetOrdersByStatusAccepted()
    {
        var result = await TestServices.ExecuteRequestAsync(
            scope =>
            {
                var context = scope.ServiceProvider.GetRequiredService<AdSpotDbContext>();
                context.Connections.Add(
                    new Connection
                    {
                        UserId = TestDatabase.TestUser.UserId,
                        PlatformId = TestDatabase.Platforms.First().PlatformId,
                        Handle = "TestAccountHandle",
                        Token = "TestApiToken"
                    }
                );
                context.SaveChanges();

                context.Listings.Add(
                    new Listing
                    {
                        ListingId = 1,
                        PlatformId = TestDatabase.Platforms.First().PlatformId,
                        UserId = TestDatabase.TestUser.UserId,
                        ListingTypeId = TestDatabase
                            .ListingTypes.First(listingType =>
                                listingType.PlatformId == TestDatabase.Platforms.First().PlatformId
                            )
                            .PlatformId
                    }
                );
                context.SaveChanges();

                context.Orders.Add(
                    new Order
                    {
                        OrderId = 1,
                        ListingId = 1,
                        UserId = TestDatabase.TestUser.UserId,
                        Price = 100.00M,
                        Description = "Test Order",
                        OrderStatusId = OrderStatusEnum.Accepted
                    }
                );
                context.SaveChanges();
            },
            b =>
                b.SetDocument(GetOrdersByStatusQuery)
                    .SetVariableValues(
                        new Dictionary<string, object?>
                        {
                            { "userId", TestDatabase.TestUser.UserId },
                            { "status", "ACCEPTED" }
                        }.AsReadOnly()
                    )
        );

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetOrdersByStatusRejected()
    {
        var result = await TestServices.ExecuteRequestAsync(
            scope =>
            {
                var context = scope.ServiceProvider.GetRequiredService<AdSpotDbContext>();
                context.Connections.Add(
                    new Connection
                    {
                        UserId = TestDatabase.TestUser.UserId,
                        PlatformId = TestDatabase.Platforms.First().PlatformId,
                        Handle = "TestAccountHandle",
                        Token = "TestApiToken"
                    }
                );
                context.SaveChanges();

                context.Listings.Add(
                    new Listing
                    {
                        ListingId = 1,
                        PlatformId = TestDatabase.Platforms.First().PlatformId,
                        UserId = TestDatabase.TestUser.UserId,
                        ListingTypeId = TestDatabase
                            .ListingTypes.First(listingType =>
                                listingType.PlatformId == TestDatabase.Platforms.First().PlatformId
                            )
                            .PlatformId
                    }
                );
                context.SaveChanges();

                context.Orders.Add(
                    new Order
                    {
                        OrderId = 1,
                        ListingId = 1,
                        UserId = TestDatabase.TestUser.UserId,
                        Price = 100.00M,
                        Description = "Test Order",
                        OrderStatusId = OrderStatusEnum.Rejected
                    }
                );
                context.SaveChanges();
            },
            b =>
                b.SetDocument(GetOrdersByStatusQuery)
                    .SetVariableValues(
                        new Dictionary<string, object?>
                        {
                            { "userId", TestDatabase.TestUser.UserId },
                            { "status", "REJECTED" }
                        }.AsReadOnly()
                    )
        );

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetOrdersByStatusCompleted()
    {
        var result = await TestServices.ExecuteRequestAsync(
            scope =>
            {
                var context = scope.ServiceProvider.GetRequiredService<AdSpotDbContext>();
                context.Connections.Add(
                    new Connection
                    {
                        UserId = TestDatabase.TestUser.UserId,
                        PlatformId = TestDatabase.Platforms.First().PlatformId,
                        Handle = "TestAccountHandle",
                        Token = "TestApiToken"
                    }
                );
                context.SaveChanges();

                context.Listings.Add(
                    new Listing
                    {
                        ListingId = 1,
                        PlatformId = TestDatabase.Platforms.First().PlatformId,
                        UserId = TestDatabase.TestUser.UserId,
                        ListingTypeId = TestDatabase
                            .ListingTypes.First(listingType =>
                                listingType.PlatformId == TestDatabase.Platforms.First().PlatformId
                            )
                            .PlatformId
                    }
                );
                context.SaveChanges();

                context.Orders.Add(
                    new Order
                    {
                        OrderId = 1,
                        ListingId = 1,
                        UserId = TestDatabase.TestUser.UserId,
                        Price = 100.00M,
                        Description = "Test Order",
                        OrderStatusId = OrderStatusEnum.Completed
                    }
                );
                context.SaveChanges();
            },
            b =>
                b.SetDocument(GetOrdersByStatusQuery)
                    .SetVariableValues(
                        new Dictionary<string, object?>
                        {
                            { "userId", TestDatabase.TestUser.UserId },
                            { "status", "COMPLETED" }
                        }.AsReadOnly()
                    )
        );

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetRequestsByStatusPending()
    {
        var result = await TestServices.ExecuteRequestAsync(
            scope =>
            {
                var context = scope.ServiceProvider.GetRequiredService<AdSpotDbContext>();
                context.Connections.Add(
                    new Connection
                    {
                        UserId = TestDatabase.TestUser.UserId,
                        PlatformId = TestDatabase.Platforms.First().PlatformId,
                        Handle = "TestAccountHandle",
                        Token = "TestApiToken"
                    }
                );
                context.SaveChanges();

                context.Listings.Add(
                    new Listing
                    {
                        ListingId = 1,
                        PlatformId = TestDatabase.Platforms.First().PlatformId,
                        UserId = TestDatabase.TestUser.UserId,
                        ListingTypeId = TestDatabase
                            .ListingTypes.First(listingType =>
                                listingType.PlatformId == TestDatabase.Platforms.First().PlatformId
                            )
                            .PlatformId
                    }
                );
                context.SaveChanges();

                context.Orders.Add(
                    new Order
                    {
                        OrderId = 1,
                        ListingId = 1,
                        UserId = TestDatabase.TestUser.UserId,
                        Price = 100.00M,
                        Description = "Test Order"
                    }
                );
                context.SaveChanges();
            },
            b =>
                b.SetDocument(GetRequestsByStatusQuery)
                    .SetVariableValues(
                        new Dictionary<string, object?>
                        {
                            { "userId", TestDatabase.TestUser.UserId },
                            { "status", "PENDING" }
                        }.AsReadOnly()
                    )
        );

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetRequestsByStatusAccepted()
    {
        var result = await TestServices.ExecuteRequestAsync(
            scope =>
            {
                var context = scope.ServiceProvider.GetRequiredService<AdSpotDbContext>();
                context.Connections.Add(
                    new Connection
                    {
                        UserId = TestDatabase.TestUser.UserId,
                        PlatformId = TestDatabase.Platforms.First().PlatformId,
                        Handle = "TestAccountHandle",
                        Token = "TestApiToken"
                    }
                );
                context.SaveChanges();

                context.Listings.Add(
                    new Listing
                    {
                        ListingId = 1,
                        PlatformId = TestDatabase.Platforms.First().PlatformId,
                        UserId = TestDatabase.TestUser.UserId,
                        ListingTypeId = TestDatabase
                            .ListingTypes.First(listingType =>
                                listingType.PlatformId == TestDatabase.Platforms.First().PlatformId
                            )
                            .PlatformId
                    }
                );
                context.SaveChanges();

                context.Orders.Add(
                    new Order
                    {
                        OrderId = 1,
                        ListingId = 1,
                        UserId = TestDatabase.TestUser.UserId,
                        Price = 100.00M,
                        Description = "Test Order",
                        OrderStatusId = OrderStatusEnum.Accepted
                    }
                );
                context.SaveChanges();
            },
            b =>
                b.SetDocument(GetRequestsByStatusQuery)
                    .SetVariableValues(
                        new Dictionary<string, object?>
                        {
                            { "userId", TestDatabase.TestUser.UserId },
                            { "status", "ACCEPTED" }
                        }.AsReadOnly()
                    )
        );

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetRequestsByStatusRejected()
    {
        var result = await TestServices.ExecuteRequestAsync(
            scope =>
            {
                var context = scope.ServiceProvider.GetRequiredService<AdSpotDbContext>();
                context.Connections.Add(
                    new Connection
                    {
                        UserId = TestDatabase.TestUser.UserId,
                        PlatformId = TestDatabase.Platforms.First().PlatformId,
                        Handle = "TestAccountHandle",
                        Token = "TestApiToken"
                    }
                );
                context.SaveChanges();

                context.Listings.Add(
                    new Listing
                    {
                        ListingId = 1,
                        PlatformId = TestDatabase.Platforms.First().PlatformId,
                        UserId = TestDatabase.TestUser.UserId,
                        ListingTypeId = TestDatabase
                            .ListingTypes.First(listingType =>
                                listingType.PlatformId == TestDatabase.Platforms.First().PlatformId
                            )
                            .PlatformId
                    }
                );
                context.SaveChanges();

                context.Orders.Add(
                    new Order
                    {
                        OrderId = 1,
                        ListingId = 1,
                        UserId = TestDatabase.TestUser.UserId,
                        Price = 100.00M,
                        Description = "Test Order",
                        OrderStatusId = OrderStatusEnum.Rejected
                    }
                );
                context.SaveChanges();
            },
            b =>
                b.SetDocument(GetRequestsByStatusQuery)
                    .SetVariableValues(
                        new Dictionary<string, object?>
                        {
                            { "userId", TestDatabase.TestUser.UserId },
                            { "status", "REJECTED" }
                        }.AsReadOnly()
                    )
        );

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetRequestsByStatusCompleted()
    {
        var result = await TestServices.ExecuteRequestAsync(
            scope =>
            {
                var context = scope.ServiceProvider.GetRequiredService<AdSpotDbContext>();
                context.Connections.Add(
                    new Connection
                    {
                        UserId = TestDatabase.TestUser.UserId,
                        PlatformId = TestDatabase.Platforms.First().PlatformId,
                        Handle = "TestAccountHandle",
                        Token = "TestApiToken"
                    }
                );
                context.SaveChanges();

                context.Listings.Add(
                    new Listing
                    {
                        ListingId = 1,
                        PlatformId = TestDatabase.Platforms.First().PlatformId,
                        UserId = TestDatabase.TestUser.UserId,
                        ListingTypeId = TestDatabase
                            .ListingTypes.First(listingType =>
                                listingType.PlatformId == TestDatabase.Platforms.First().PlatformId
                            )
                            .PlatformId
                    }
                );
                context.SaveChanges();

                context.Orders.Add(
                    new Order
                    {
                        OrderId = 1,
                        ListingId = 1,
                        UserId = TestDatabase.TestUser.UserId,
                        Price = 100.00M,
                        Description = "Test Order",
                        OrderStatusId = OrderStatusEnum.Completed
                    }
                );
                context.SaveChanges();
            },
            b =>
                b.SetDocument(GetRequestsByStatusQuery)
                    .SetVariableValues(
                        new Dictionary<string, object?>
                        {
                            { "userId", TestDatabase.TestUser.UserId },
                            { "status", "COMPLETED" }
                        }.AsReadOnly()
                    )
        );

        result.MatchSnapshot();
    }
}
