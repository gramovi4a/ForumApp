namespace Domain.Models;

public class Post
{
    public int Id { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
    public string Title { get; private set;}
    public string? Text { get; private set;}

    public Post(int userId, string title, string? text)
    {
        UserId = userId;
        Title = title;
        Text = text;
    }

    private Post()
    {
    }
}