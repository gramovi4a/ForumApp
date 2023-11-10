using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess;

public class PostEfcDao : IPostDao
{
    private readonly ForumContext context;
    
    public PostEfcDao(ForumContext context)
    {
        this.context = context;
    }

    public async Task<Post> CreateAsync(Post post)
    {
        EntityEntry<Post> added = await context.Posts.AddAsync(post);
        await context.SaveChangesAsync();
        return added.Entity;

    }

    public async Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
       
        IQueryable<Post> query = context.Posts.Include(post => post.User).AsQueryable();
        
        if (!string.IsNullOrEmpty(searchParameters.Username))
        {
            // we know username is unique, so just fetch the first
            query = query.Where(post =>
                post.User.Username.ToLower().Equals(searchParameters.Username.ToLower()));
        }
        
        if (searchParameters.UserId != null)
        {
            query = query.Where(p => p.User.Id == searchParameters.UserId);
        }
        
        if (!string.IsNullOrEmpty(searchParameters.TitleContains))
        {
            query = query.Where(p =>
                p.Title.ToLower().Contains(searchParameters.TitleContains.ToLower()));
        }
        
        List<Post> result = await query.ToListAsync();
        return result;
    }

    public async Task UpdateAsync(Post post)
    {
        context.Posts.Update(post);
        await context.SaveChangesAsync();
    }

    public async Task<Post> GetByIdAsync(int id)
    {
        Post? found = await context.Posts
            .Include(post => post.User)
            .SingleOrDefaultAsync(post => post.Id == id);
        return found;
    }

    public async Task DeleteAsync(int id)
    {
       
        Post? existing = await GetByIdAsync(id);
        if (existing == null)
        {
            throw new Exception($"Post with id {id} not found");
        }
        
        context.Posts.Remove(existing);
        await context.SaveChangesAsync();
    }
}