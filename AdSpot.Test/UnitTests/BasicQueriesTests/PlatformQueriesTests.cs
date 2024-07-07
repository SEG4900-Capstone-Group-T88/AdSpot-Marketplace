namespace AdSpot.Test.UnitTests.BasicQueriesTests;

[Collection("adspot-inmemory-db")]
public class PlatformQueriesTests
{
    private const string PlatformsQuery = """
        query {
            platforms {
                platformId
                name
                listingTypes {
                    listingTypeId
                    name
                }
            }
        }
        """;

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetPlatforms()
    {
        var result = await TestServices.ExecuteRequestAsync(b => b.SetQuery(PlatformsQuery));

        result.MatchSnapshot();
    }
}
