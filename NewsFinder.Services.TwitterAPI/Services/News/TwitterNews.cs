using Microsoft.Extensions.Options;
using NewsFinder.Services.TwittersAPI.DTO;
using Tweetinvi;

namespace NewsFinder.Services.TwittersAPI.Services.News;

public class TwitterNews : ITwitterNews
{
    private readonly TwitterClient _client;
    private readonly HttpClient _httpClient;

    public TwitterNews(IOptions<TwitterSettings> options, HttpClient httpClient)
    {
        _httpClient = httpClient;
        _client = new TwitterClient(
            options.Value.CONSUMER_KEY, 
            options.Value.CONSUMER_SECRET, 
            options.Value.ACCESS_TOKEN, 
            options.Value.ACCESS_TOKEN_SECRET);
        
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {options.Value.ACCESS_TOKEN}");
    }

    public async Task<TwitterTweetDto?> GetLatestTweetAsync(string username)
    {
        // Get user details using v2 API to obtain the user's ID
        var userResponse = await _client.UsersV2.GetUserByNameAsync(username);
        if (userResponse == null || userResponse.User == null)
            return null;

        var userId = userResponse.User.Id;

        // Fetch the user's timeline to get the latest tweet using the user's ID
        var url = $"https://api.twitter.com/2/tweets/timelines/user/{userId}?max_results=1";
        var tweetsResponse = await _client.TweetsV2.GetTweetsAsync(userId.ToString());

        var latestTweet = tweetsResponse?.Tweets?.FirstOrDefault();

        if (latestTweet == null)
            return null;

        return new TwitterTweetDto
        {
            CreatedAt = Convert.ToDateTime(latestTweet.CreatedAt.ToLocalTime()),
            TweetId = latestTweet.Id,
            Text = latestTweet.Text
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

public class TweetV2Response
{
    public TweetData Data { get; set; }
}

public class TweetData
{
    public string Text { get; set; }
}