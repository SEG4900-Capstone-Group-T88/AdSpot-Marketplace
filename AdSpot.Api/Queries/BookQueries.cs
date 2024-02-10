namespace AdSpot.Api.Queries;

[QueryType]
public class BookQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Book> GetBooks(BookRepository repo)
    {
        return repo.GetAllBooks();
    }
}
