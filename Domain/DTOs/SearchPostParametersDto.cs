using System.Net.Mime;

namespace Domain.DTOs;

public class SearchPostParametersDto//not used here
{
    public string? Username { get;}
    public int? UserId { get;}
    public string? TitleContains { get;}
    public string? TextContains { get; }

    public SearchPostParametersDto(string? username, int? userId, string? titleContains, string? textContains)
    {
        Username = username;
        UserId = userId;
        TitleContains = titleContains;
        TextContains = textContains;
    }
}