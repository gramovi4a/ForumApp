namespace Domain.DTOs;

public class PostBasicDto
{
    public int Id { get; }
    public string UserName { get; }
    public string Title { get; }
    public string Text { get; }

    public PostBasicDto(int id, string userName, string title, string text)
    {
        Id = id;
        UserName = userName;
        Title = title;
        Text = text;
    }
}