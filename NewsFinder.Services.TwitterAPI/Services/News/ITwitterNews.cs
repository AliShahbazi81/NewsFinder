using NewsFinder.Services.TwittersAPI.DTO;
using Tweetinvi.Models;

namespace NewsFinder.Services.TwittersAPI.Services.News;

public interface ITwitterNews
{
    Task<TwitterTweetDto?> GetLatestTweetAsync(string username);
    Task<TwitterUserDto?> GetUserCredentials(string screenName);
}