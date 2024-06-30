namespace AdSpot.Test.UnitTests.UserMutationsTests;

public class AddUserMutationTests
{
    private const string AddUserMutation = """
        mutation AddUser($input: AddUserInput!) {
            addUser(input: $input) {
                user {
                    userId
                    email
                    firstName
                    lastName
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
    public async Task AddUserSuccessful()
    {
        var result = await TestServices.ExecuteRequestAsync(b =>
            b.SetQuery(AddUserMutation)
                .SetVariableValue(
                    "input",
                    new Dictionary<string, object?>
                    {
                        { "email", "newuser" },
                        { "password", "newuser" },
                        { "firstName", "new" },
                        { "lastName", "user" },
                    }
                )
        );

        result.MatchSnapshot();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task AddUserEmailAlreadyExists()
    {
        var result = await TestServices.ExecuteRequestAsync(b =>
            b.SetQuery(AddUserMutation)
                .SetVariableValue(
                    "input",
                    new Dictionary<string, object?>
                    {
                        { "email", "user1" },
                        { "password", "user1" },
                        { "firstName", "user" },
                        { "lastName", "1" },
                    }
                )
        );

        result.MatchSnapshot();
    }
}
