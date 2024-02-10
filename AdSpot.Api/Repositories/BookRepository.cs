namespace AdSpot.Api.Repositories;

public class BookRepository
{
    private readonly AdSpotDbContext context;

    public BookRepository(AdSpotDbContext context)
    {
        this.context = context;
    }

    public IQueryable<Book> GetAllBooks()
    {
        return context.Books;
    }
}
