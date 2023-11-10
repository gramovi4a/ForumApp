using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    Task<User> CreateAsync(User user);
    Task<User?> GetUserAsync(string username, string password);
    Task<User?> GetByUsernameAsync(string username);
    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);
    Task<User?> GetByIdAsync(int id);
}