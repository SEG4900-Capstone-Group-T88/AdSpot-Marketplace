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

    public User AddUser(User user)
    {
        context.Users.Add(user);
        context.SaveChanges();
        return user;
    }

    public User ValidateUser(String email, String password) {
        return context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
    }
}