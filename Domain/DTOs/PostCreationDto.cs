using System.Net.Mime;

namespace Domain.DTOs;
public class PostCreationDto
{
    public int UserId { get; }
    public string Title { get; }
    public string? Text { get; set; }


    public PostCreationDto(int userId, string title, string? text)
    {
        UserId = userId;
        Title = title;
        Text = text;
    }
}