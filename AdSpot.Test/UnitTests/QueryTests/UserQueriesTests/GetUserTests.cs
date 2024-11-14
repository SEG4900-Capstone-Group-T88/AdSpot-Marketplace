namespace AdSpot.Test.UnitTests.UserQueriesTests;

[Collection("adspot-inmemory-db")]
public class GetUserTests
{
    public const string GetUserQuery = """
        query GetUser ($userId:Int!) {
            userById (userId: $userId) {
                userId
                firstName
                lastName
                email
            }
        }
        """;

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetUserSuccessful()
    {
        var result = await TestServices.ExecuteRequestAsync(b =>
            b.SetDocument(GetUserQuery)
                .SetVariableValues(
                    new Dictionary<string, object?> { { "userId", TestDatabase.TestUser.UserId } }.AsReadOnly()
                )
        );

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetUserNotFound()
    {
        var result = await TestServices.ExecuteRequestAsync(b =>
            b.SetDocument(GetUserQuery)
                .SetVariableValues(new Dictionary<string, object?> { { "userId", -1 } }.AsReadOnly())
        );

        result.MatchSnapshot();
    }
}
