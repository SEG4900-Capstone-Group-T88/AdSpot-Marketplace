namespace AdSpot.Api.Queries;

[QueryType]
public class AuthorQueries
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Author> GetAuthors(AuthorRepository repo)
    {
        return repo.GetAllAuthors();
    }
}
