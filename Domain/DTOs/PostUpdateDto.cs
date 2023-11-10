namespace Domain.DTOs;

public class PostUpdateDto //not used here
{
    public int Id { get; }
    public int? UserId { get; set; }
    public string? Title { get; set; }
    public string Text { get; set; }

    public PostUpdateDto(int id)
    {
        Id = id;
    }
}