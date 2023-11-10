using System.ComponentModel.DataAnnotations;
using Application.DaoInterfaces;
using Domain.Models;

namespace WebApi.Services;

public class AuthService : IAuthService
{
    private readonly IUserDao dao;

    public AuthService(IUserDao userDao)
    {
        dao = userDao;
    }
    public async Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = await dao.GetByUsernameAsync(username);
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return existingUser;
    }
    
}