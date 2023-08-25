using Microsoft.Extensions.Options;
using NewsFinder.Services.TwittersAPI.DTO;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace NewsFinder.Services.TwittersAPI.Services.News;

public class TwitterNews : ITwitterNews
{
    private readonly TwitterClient _client;

    public TwitterNews(IOptions<TwitterSettings> options)
    {
        _client = new TwitterClient(
            options.Value.CONSUMER_KEY, 
            options.Value.CONSUMER_SECRET, 
            options.Value.ACCESS_TOKEN, 
            options.Value.ACCESS_TOKEN_SECRET);
    }

    public async Task<TwitterTweetDto?> GetLatestTweetAsync(string username)
    {
        var userTimelineParameters = new GetUserTimelineParameters(username)
        {
            PageSize = 1
        };

        var tweets = await _client.Timelines.GetUserTimelineAsync(userTimelineParameters);

        var latestTweet = tweets.FirstOrDefault();

        if (latestTweet == null)
            return null;

        return new TwitterTweetDto
        {
            CreatedAt = Convert.ToDateTime(latestTweet.CreatedAt.ToLocalTime()),
            TweetId = latestTweet.Id,
            Text = latestTweet.Text,
            Url = latestTweet.Url,
            MediaUrl = latestTweet.Media,
        };

    }

    public async Task<TwitterUserDto?> GetUserCredentials(string screenName)
    {
        // var user = await _client.Users.GetAuthenticatedUserAsync();
        var user = await _client.Users.GetUserAsync(screenName);
        if (user is not null)
        {
            return new TwitterUserDto
            {
                Id = user.Id,
                Name = user.Name,
                ScreenName = user.ScreenName,
                Location = user.Location,
                Description = user.Description
            };
        }
        return null;
    }
}