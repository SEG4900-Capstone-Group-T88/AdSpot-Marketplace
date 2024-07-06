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

    public IQueryable<User> DeleteUser(int userId)
    {
        var user = context.Users.FirstOrDefault(u => u.UserId == userId);
        if (user is not null)
        {
            context.Users.Remove(user);
            context.SaveChanges();
        }
        return context.Users.Where(u => u.UserId == userId);
    }

    public IQueryable<User> UpdatePassword(int userId, string password)
    {
        var user = context.Users.FirstOrDefault(u => u.UserId == userId);
        if (user is not null)
        {
            user.Password = password;
            context.SaveChanges();
        }
        return context.Users.Where(u => u.UserId == userId);
    }

    public User? ValidateUser(string email, string password)
    {
        return context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
    }
}
