using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao userDao;

    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }
    public async Task<User> CreateAsync(UserCreationDto dto)
    {
      User? existing = await userDao.GetByUsernameAsync(dto.Username);
      // User? existing = await userDao.GetUserAsync(userToCreate.Username, userToCreate.Password);

        if (existing != null)
            throw new Exception("Username already taken!");

        ValidateData(dto);
        
        User toCreate = new User
        {
            Username = dto.Username,
            Password = dto.Password
        };
    
        User created = await userDao.CreateAsync(toCreate);
    
        return created;
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        return userDao.GetAsync(searchParameters);
    }

    public async Task<User?> GetUserAsync(string userName, string password)
    {
        return await userDao.GetUserAsync(userName, password);
    }

    private static void ValidateData(UserCreationDto userToCreate)
    {
        string userName = userToCreate.Username;
        string password = userToCreate.Password;

        if (userName.Length < 3)
            throw new Exception("Username must be at least 3 characters!");

        if (userName.Length > 15)
            throw new Exception("Username must be less than 16 characters!");

        // validate password!!!!
    }
}