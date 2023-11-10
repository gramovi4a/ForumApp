using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace FileData.DAOs;

public class PostFileDao: IPostDao
{
    
    private readonly FileContext context;

    public PostFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<Post> CreateAsync(Post post)
    {
        int postId = 1;
        if (context.Posts.Any())
        {
            postId = context.Posts.Max(t => t.Id); 
            postId++;
        }

        post.Id = postId;
        
        context.Posts.Add(post);
        context.SaveChanges();

        return Task.FromResult(post);
    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        IEnumerable<Post> result = context.Posts.AsEnumerable();

        if (!string.IsNullOrEmpty(searchParameters.Username))
        {
            result = context.Posts.Where(post =>
                post.User.Username.Equals(searchParameters.Username, StringComparison.OrdinalIgnoreCase));
        }

        if (searchParameters.UserId != null)
        {
            result = context.Posts.Where(post => post.User.Id == searchParameters.UserId);
        }
        
        if (!string.IsNullOrEmpty(searchParameters.TitleContains))
        {
            result = context.Posts.Where(post =>
                post.Title.Contains(searchParameters.TitleContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(result);
    }

    public Task UpdateAsync(Post toUpdate)
    {
        Post? existing = context.Posts.FirstOrDefault(post => post.Id == toUpdate.Id);

        if (existing == null)
        {
            throw new Exception($"Post with id {toUpdate.Id} does not exist!");
        }

        context.Posts.Remove(existing);
        context.Posts.Add(toUpdate);
        
        context.SaveChanges();

        return Task.CompletedTask;
    }

    public Task<Post> GetByIdAsync(int id)
    {
        Post? existing = context.Posts.FirstOrDefault(post => post.Id == id);
        return Task.FromResult(existing);
    }

    public Task DeleteAsync(int id)
    {
        Post? existing = context.Posts.FirstOrDefault(post => post.Id == id);
        if (existing == null)
        {
            throw new Exception($"Post with id {id} does not exist!");
        }

        context.Posts.Remove(existing);
        context.SaveChanges();

        return Task.CompletedTask;
    }
}