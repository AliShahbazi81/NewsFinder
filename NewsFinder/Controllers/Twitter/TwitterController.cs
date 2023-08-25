using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NewsFinder.Services.TwittersAPI.DTO;
using NewsFinder.Services.TwittersAPI.Services.News;

namespace TweetsAndNewsOnTelegrm.Controllers.Twitter;

[ApiController]
[Route("api/[controller]")]
public class TwitterController : Controller
{
    private readonly ITwitterNews _twitterNews;
    private readonly ILogger<TwitterController> _logger;
    
    public TwitterController(ITwitterNews twitterNews, ILogger<TwitterController> logger)
    {
        _twitterNews = twitterNews;
        _logger = logger;
    }
    
    [HttpPost("GetLatestTweet")]
    public async Task<IActionResult> GetLatestTweet(string username)
    {
        try
        {
            var tweet = await _twitterNews.GetLatestTweetAsync(username);
            return Ok(tweet);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = "An error occurred while processing your request.", details = ex.Message });
            _logger.LogWarning(ex.Message);
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    [HttpGet("GetUserCredentials")]
    public async Task<IActionResult> GetUserCredentials(string screenName)
    {
        try
        {
            var user = await _twitterNews.GetUserCredentials(screenName);
            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogWarning("Error getting user's credentials" + ex.Message);
            Console.WriteLine(ex.Message);
            return BadRequest(new { error = "An error occurred while processing your request.", details = ex.Message });
        }
        
    }
}