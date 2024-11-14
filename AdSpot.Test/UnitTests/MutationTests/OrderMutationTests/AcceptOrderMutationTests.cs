﻿using AdSpot.Models;

namespace AdSpot.Test.UnitTests.OrderMutationsTests;

[Collection("adspot-inmemory-db")]
public class AcceptOrderMutationsTests
{
    public const string AcceptOrderMutation = """
            mutation AcceptOrder($input: AcceptOrderInput!) {
                acceptOrder(input: $input) {
                    order {
                        listingId
                        userId
                        orderId
                        description
                    }
                    errors {
                        ... on Error {
                            __typename
                            message
                        }
                    }
                }
            }
        """;

    [Fact]
    [Trait("Category", "Unit")]
    public async Task AcceptOrderSuccessful()
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
                            .PlatformId,
                        Price = 100.00M
                    }
                );
                context.SaveChanges();

                context.Users.Add(
                    new User
                    {
                        UserId = 2,
                        Email = "testuser2@adspot.com",
                        Password = "testuserpassword2",
                        FirstName = "Test",
                        LastName = "User 2"
                    }
                );
                context.SaveChanges();

                context.Orders.Add(
                    new Order
                    {
                        OrderId = 1,
                        ListingId = 1,
                        UserId = 2,
                        Price = 100.00M,
                        Description = "Test Order"
                    }
                );
                context.SaveChanges();
            },
            b =>
                b.SetDocument(AcceptOrderMutation)
                    .SetVariableValues(
                        new Dictionary<string, object?>
                        {
                            {
                                "input",
                                new Dictionary<string, object?>
                                {
                                    { "orderId", 1 },
                                    { "userId", TestDatabase.TestUser.UserId }
                                }
                            }
                        }.AsReadOnly()
                    )
        );

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task AcceptOrderInvalidOrderIdError()
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
                            .PlatformId,
                        Price = 100.00M
                    }
                );
                context.SaveChanges();

                context.Users.Add(
                    new User
                    {
                        UserId = 2,
                        Email = "testuser2@adspot.com",
                        Password = "testuserpassword2",
                        FirstName = "Test",
                        LastName = "User 2"
                    }
                );
                context.SaveChanges();

                context.Orders.Add(
                    new Order
                    {
                        OrderId = 1,
                        ListingId = 1,
                        UserId = 2,
                        Price = 100.00M,
                        Description = "Test Order"
                    }
                );
                context.SaveChanges();
            },
            b =>
                b.SetDocument(AcceptOrderMutation)
                    .SetVariableValues(
                        new Dictionary<string, object?>
                        {
                            {
                                "input",
                                new Dictionary<string, object?>
                                {
                                    { "orderId", -1 },
                                    { "userId", TestDatabase.TestUser.UserId }
                                }
                            }
                        }.AsReadOnly()
                    )
        );

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task AcceptOrderUserIdError()
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
                            .PlatformId,
                        Price = 100.00M
                    }
                );
                context.SaveChanges();

                context.Users.Add(
                    new User
                    {
                        UserId = 2,
                        Email = "testuser2@adspot.com",
                        Password = "testuserpassword2",
                        FirstName = "Test",
                        LastName = "User 2"
                    }
                );
                context.SaveChanges();

                context.Orders.Add(
                    new Order
                    {
                        OrderId = 1,
                        ListingId = 1,
                        UserId = 2,
                        Price = 100.00M,
                        Description = "Test Order"
                    }
                );
                context.SaveChanges();
            },
            b =>
                b.SetDocument(AcceptOrderMutation)
                    .SetVariableValues(
                        new Dictionary<string, object?>
                        {
                            {
                                "input",
                                new Dictionary<string, object?> { { "orderId", 1 }, { "userId", 2 } }
                            }
                        }.AsReadOnly()
                    )
        );

        result.MatchSnapshot();
    }
}
