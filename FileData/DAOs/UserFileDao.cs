using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace FileData.DAOs;

public class UserFileDao : IUserDao
{
    private readonly FileContext context;

    public UserFileDao(FileContext context)
    {
        this.context = context;
    }
    public Task<User> CreateAsync(User user)
    {
        int userId = 1;
        if (context.Users.Any())
        {
            userId = context.Users.Max(u => u.Id);
            userId++;
        }

        user.Id = userId;
        Console.WriteLine($"Username: {user.Username}, Password: {user.Password}");
        context.Users.Add(user);
        context.SaveChanges();

        return Task.FromResult(user);
    }

    public Task<User?> GetUserAsync(string username, string password)
    {
        User? existing = context.Users.FirstOrDefault(u => u.Username
                                                               .Equals(username, StringComparison.OrdinalIgnoreCase) &&
                                                           u.Password.Equals(password,
                                                               StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(existing);
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        User? existing = context.Users.FirstOrDefault(u =>
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
        );
        return Task.FromResult(existing);
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        IEnumerable<User> users = context.Users.AsEnumerable();
        if (searchParameters.UsernameContains != null)
        {
            users = context.Users.Where(u =>
                u.Username.Contains(searchParameters.UsernameContains, StringComparison.OrdinalIgnoreCase));
            
        }

        return Task.FromResult(users);
    }

    public Task<User?> GetByIdAsync(int id)
    {
        User? existing = context.Users.FirstOrDefault(u => u.Id == id);

        return Task.FromResult(existing);
    }
}