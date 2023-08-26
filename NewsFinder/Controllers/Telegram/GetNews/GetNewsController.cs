using Microsoft.AspNetCore.Mvc;
using NewsFinder.Services.TelegramAPI.Services.News.GetPosts;

namespace TweetsAndNewsOnTelegrm.Controllers.Telegram.GetNews;

[ApiController]
[Route("api/[controller]")]
public class GetNewsController : Controller
{
    private readonly IGetNews _getNews;
    private readonly ILogger<GetNewsController> _logger;
    
    public GetNewsController(IGetNews getNews, ILogger<GetNewsController> logger)
    {
        _getNews = getNews;
        _logger = logger;
    }
    
    [HttpGet("GetLatestPost")]
    public async Task<IActionResult> GetLatestPost()
    {
        try
        {
            var posts = await _getNews.GetLatestPostAsync();
            return Ok(posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _logger.LogWarning(e.Message);
            throw;
        }
    }
}