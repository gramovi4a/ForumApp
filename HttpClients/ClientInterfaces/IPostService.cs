using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
    Task CreateAsync(PostCreationDto dto);
    Task<ICollection<Post>> GetAsync(
        string? username, 
        int? userId, 
        string? title, 
        string? text
        
    );
    Task<PostBasicDto> GetByIdAsync(int id);
}