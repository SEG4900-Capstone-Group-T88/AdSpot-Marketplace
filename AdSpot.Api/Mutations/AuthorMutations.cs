namespace AdSpot.Api.Mutations;

[MutationType]
public class AuthorMutations
{
    public Author AddAuthor(AuthorRepository repo, Author author)
    {
        repo.AddAuthor(author);
        return author;
    }
}
