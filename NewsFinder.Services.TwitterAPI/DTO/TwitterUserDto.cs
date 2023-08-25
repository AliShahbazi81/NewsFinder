namespace NewsFinder.Services.TwittersAPI.DTO;

public class TwitterUserDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ScreenName { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty; 
    public string Description { get; set; } = string.Empty;
    
}