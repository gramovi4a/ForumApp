using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDao postDao;
    private readonly IUserDao userDao;

    public PostLogic(IPostDao postDao, IUserDao userDao)
    {
        this.postDao = postDao;
        this.userDao = userDao;
    }
    
    public async Task<Post> CreateAsync(PostCreationDto dto)
    {
        User? user = await userDao.GetByIdAsync(dto.UserId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.UserId} was not found.");
        }

        Post post = new Post(user.Id, dto.Title, dto.Text);
        
        ValidatePost(post);
        
        Post created = await postDao.CreateAsync(post);
        return created;
    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        return postDao.GetAsync(searchParameters);
    }

    public async Task UpdateAsync(PostUpdateDto dto)
    {
        Post? existing = await postDao.GetByIdAsync(dto.Id);

        if (existing == null)
        {
            throw new Exception($"Post with ID {dto.Id} not found!");
        }

        User? user = null;
        if (dto.UserId != null)
        {
            user = await userDao.GetByIdAsync((int)dto.UserId);
            if (user == null)
            {
                throw new Exception($"User with id {dto.UserId} was not found.");
            }
        }

        User userToUse = user ?? existing.User;
        string titleToUse = dto.Title ?? existing.Title;
        string textToUse = dto.Text ?? existing.Text;

        Post updated = new(userToUse.Id, titleToUse, textToUse)
        {
            Id = existing.Id,
        };
            
        ValidatePost(updated);

        await postDao.UpdateAsync(updated);
    }

    public async Task DeleteAsync(int id)
    {
        Post? post = await postDao.GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"Post with ID {id} was not found!");
        }

        await postDao.DeleteAsync(id);
    }

    public async Task<PostBasicDto> GetByIdAsync(int id)
    {
        Post? post = await postDao.GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"Post with ID {id} not found");
        }
        return new PostBasicDto(post.Id, post.User.Username, post.Title, post.Text);
    }
    
    private void ValidatePost(Post dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Post's title cannot be empty.");
        if (string.IsNullOrEmpty(dto.Text)) throw new Exception("Post's body cannot be empty.");
        // other validation stuff text cant be null!!
    }
}