using AdSpot.Models;

namespace AdSpot.Test;

public static class TestDatabase
{
    public static User TestUser =
        new()
        {
            UserId = 1,
            Email = "testuser@adspot.com",
            Password = "testuserpassword",
            FirstName = "Test",
            LastName = "User"
        };

    public static List<Platform> Platforms =
    [
        new() { PlatformId = 1, Name = "Facebook" },
        new() { PlatformId = 2, Name = "Twitter" },
        new() { PlatformId = 3, Name = "Instagram" },
        new() { PlatformId = 4, Name = "Youtube" }
    ];

    public static List<ListingType> ListingTypes =
    [
        new()
        {
            ListingTypeId = 1,
            Name = "Post",
            PlatformId = 1
        },
        new()
        {
            ListingTypeId = 2,
            Name = "Share",
            PlatformId = 1
        },
        new()
        {
            ListingTypeId = 3,
            Name = "Tweet",
            PlatformId = 2
        },
        new()
        {
            ListingTypeId = 4,
            Name = "Retweet",
            PlatformId = 2
        },
        new()
        {
            ListingTypeId = 5,
            Name = "Story",
            PlatformId = 3
        },
        new()
        {
            ListingTypeId = 6,
            Name = "Post",
            PlatformId = 3
        },
        new()
        {
            ListingTypeId = 7,
            Name = "Video",
            PlatformId = 4
        },
        new()
        {
            ListingTypeId = 8,
            Name = "Stream",
            PlatformId = 4
        }
    ];

    public static void SeedTestDatabase(this AdSpotDbContext context)
    {
        context.Database.EnsureCreated();

        context.Users.Add(TestUser);
        context.SaveChanges();

        context.Platforms.AddRange(Platforms);
        context.SaveChanges();

        context.ListingTypes.AddRange(ListingTypes);
        context.SaveChanges();
    }
}
