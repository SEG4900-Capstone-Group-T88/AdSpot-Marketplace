namespace AdSpot.Api.Repositories;

public class UserRepository
{
    private readonly AdSpotDbContext context;

    public UserRepository(AdSpotDbContext context)
    {
        this.context = context;
    }

    public IQueryable<User> GetAllUsers()
    {
        return context.Users;
    }

    public User? GetUserByEmail(string email)
    {
        return context.Users.FirstOrDefault(u => u.Email == email);
    }

    public IQueryable<User> GetUserById(int userId)
    {
        return context.Users.Where(u => u.UserId == userId);
    }

    public User AddUser(User user)
    {
        context.Users.Add(user);
        context.SaveChanges();
        return user;
    }

    public User? ValidateUser(string email, string password)
    {
        return context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
    }
}