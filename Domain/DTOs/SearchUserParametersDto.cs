namespace Domain.DTOs;

public class SearchUserParametersDto//not used here
{
    public string? UsernameContains { get; }

    public SearchUserParametersDto(string? usernameContains)
    {
        UsernameContains = usernameContains;
    }
}