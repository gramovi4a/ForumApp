using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess;

public class UserEfcDao : IUserDao
{
    private readonly ForumContext context;
    
    public UserEfcDao(ForumContext context)
    {
        this.context = context;
    }

    public async Task<User> CreateAsync(User user)
    {
        EntityEntry<User> newUser = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return newUser.Entity;
    
    }

    public async Task<User?> GetUserAsync(string username, string password)
    {
        throw new NotImplementedException();
        //
        // User? existing = await context.Users.FirstOrDefaultAsync(u =>
        //     u.Username.ToLower().Equals(username.ToLower()) && context.Users.FirstOrDefaultAsync(p => 
        //         p.Password.ToLower().Equals(password.ToLower())
        // );
        // return existing;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        User? existing = await context.Users.FirstOrDefaultAsync(u =>
            u.Username.ToLower().Equals(username.ToLower())
        );
        return existing;

    }

    public async Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        IQueryable<User> usersQuery = context.Users.AsQueryable();
        if (searchParameters.UsernameContains != null)
        {
            usersQuery = usersQuery.Where(u => u.Username.ToLower().Contains(searchParameters.UsernameContains.ToLower()));
        }
        
        IEnumerable<User> result = await usersQuery.ToListAsync();
        return result;

    }

    public async Task<User?> GetByIdAsync(int id)
    {
        User? user = await context.Users.FindAsync(id);
        return user;

    }
}
