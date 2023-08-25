using Tweetinvi.Models.Entities;

namespace NewsFinder.Services.TwittersAPI.DTO;

public class TwitterTweetDto
{
    // public TwitterUserDto User { get; set; }
    public DateTime CreatedAt { get; set; }
    public long TweetId { get; set; }
    public string Text { get; set; } = string.Empty;
    public string? Url { get; set; }
    // public string? ExpandedUrl { get; set; }
    // public string? DisplayUrl { get; set; }
    public List<IMediaEntity> MediaUrl { get; set; }
    public string? MediaUrlHttps { get; set; }
    public string? Type { get; set; }
    public string? VideoUrl { get; set; }
}