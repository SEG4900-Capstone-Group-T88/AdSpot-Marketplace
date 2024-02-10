namespace AdSpot.Api.Repositories;

public class AuthorRepository
{
    private readonly AdSpotDbContext context;

    public AuthorRepository(AdSpotDbContext context)
    {
        this.context = context;
    }

    public IQueryable<Author> GetAllAuthors()
    {
        return context.Authors;
    }

    public Author AddAuthor(Author author)
    {
        context.Authors.Add(author);
        context.SaveChanges();
        return author;
    }
}
