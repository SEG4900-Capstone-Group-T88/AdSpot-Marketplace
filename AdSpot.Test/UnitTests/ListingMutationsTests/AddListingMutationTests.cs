using Xunit.Abstractions;

namespace AdSpot.Test.UnitTests.ListingMutationsTests;

public class AddListingMutationTests
{
    private const string AddListingMutation = """
    mutation AddListing($input: AddListingInput!) {
        addListing(input: $input) {
            listing {
                listingId
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

    private readonly ITestOutputHelper output;
    public AddListingMutationTests(ITestOutputHelper output)
    {
        this.output = output;
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task AddListingSuccessful()
    {
        var userId = await AddUserAsync(nameof(AddListingSuccessful));
        await AddConnectionAsync(userId, 1);

        var result = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery(AddListingMutation)
                .SetVariableValue("input", new Dictionary<string, object?>
                {
                    { "listingTypeId", 1 },
                    { "userId", userId },
                    { "price", (decimal)9.99 }
                }.AsReadOnly()));

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task AddListingInvalidListingTypeId()
    {
        var result = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery(AddListingMutation)
                .SetVariableValue("input", new Dictionary<string, object?>
                {
                    { "listingTypeId", -1 },
                    { "userId", 1 },
                    { "price", (decimal)9.99 }
                }.AsReadOnly()));

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task AddListingAccountHasNotBeenConnected()
    {
        var userId = await AddUserAsync(nameof(AddListingAccountHasNotBeenConnected));

        var result = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery(AddListingMutation)
                .SetVariableValue("input", new Dictionary<string, object?>
                {
                    { "listingTypeId", 1 },
                    { "userId", userId },
                    { "price", (decimal)9.99 }
                }.AsReadOnly()));

        result.MatchSnapshot();
    }

    private async Task<int> AddUserAsync(string email)
    {
        var addUserResult = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery("""
                mutation {
                    addUser(input: {
                        email: "EMAIL_VARIABLE",
                        password: "newuser@gmail.com",
                        firstName: "new"
                        lastName: "user"
                     }) {
                        user {
                            userId
                        }
                        errors {
                            ... on Error {
                                __typename
                                message
                            }
                        }
                    }
                }
                """.Replace("EMAIL_VARIABLE", email)));
        var json = addUserResult.ToJson();
        output.WriteLine(json);
        var obj = JsonConvert.DeserializeObject<JObject>(json);
        var userIdToken = obj?["data"]?["addUser"]?["user"]?["userId"];

        Assert.NotNull(userIdToken);
        return (int)userIdToken;
    }
    private async Task AddConnectionAsync(int userId, int platformId)
    {
        var addUserResult = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery("""
                mutation AddConnection($input: AddConnectionInput!) {
                    addConnection(input: $input) {
                        connection {
                            platformId
                        }
                    }
                }
                """)
            .SetVariableValue("input", new Dictionary<string, object?>
            {
                { "userId", userId },
                { "platformId", platformId },
                { "accountHandle", "testHandle" },
                { "apiToken", "testToken" }
            }.AsReadOnly()));
        var json = addUserResult.ToJson();
        var obj = JsonConvert.DeserializeObject<JObject>(json);
        var platformIdToken = obj?["data"]?["addConnection"]?["connection"]?["platformId"];

        Assert.NotNull(platformIdToken);
    }
}
