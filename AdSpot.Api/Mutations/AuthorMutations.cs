namespace AdSpot.Api.Mutations;

[MutationType]
public class AuthorMutations
{
    [Error<NameCannotBeBlankError>]
    [Error<NameCannotContainNumbersError>]
    [Error<NameCannotContainPunctuationError>]
    public MutationResult<Author> AddAuthor(AuthorRepository repo, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return new(new NameCannotBeBlankError());
        }

        var errors = new List<object>();

        if (name.Any(char.IsDigit))
        {
            errors.Add(new NameCannotContainNumbersError());
        }

        if (name.Any(char.IsPunctuation))
        {
            errors.Add(new NameCannotContainPunctuationError());
        }

        if (errors.Count > 0)
        {
            return new(errors);
        }

        var author = repo.AddAuthor(new Author { Name = name });
        return author;
    }
}

public record NameCannotBeBlankError()
{
    public string Message => "Name cannot be blank.";
}

public record NameCannotContainPunctuationError()
{
    public string Message => "Name cannot contain punctuation.";
}

public record NameCannotContainNumbersError()
{
    public string Message => "Name cannot contain numbers.";
}
