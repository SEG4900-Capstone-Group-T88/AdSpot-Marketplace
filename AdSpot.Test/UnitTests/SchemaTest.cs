using CookieCrumble;

namespace AdSpot.Test.UnitTests;

public class SchemaTest
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task SchemaChangeTest()
    {
        var schema = await TestServices.Executor.GetSchemaAsync(default);
        schema.MatchSnapshot();
    }
}
