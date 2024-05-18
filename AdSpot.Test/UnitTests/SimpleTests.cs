namespace AdSpot.Test.UnitTests;

public class SimpleTests
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetPlatforms()
    {
        var result = await TestServices.ExecuteRequestAsync(
            b => b.SetQuery("""
                {
                    platforms {
                        platformId
                        name
                        listingTypes {
                            listingTypeId
                            name
                        }
                    }
                }
                """));
        result.MatchSnapshot();
    }
}
