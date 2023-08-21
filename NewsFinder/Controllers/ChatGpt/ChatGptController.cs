using Microsoft.AspNetCore.Mvc;
using NewsFinder.Services.ChatGPT.Services.ChatGPT;

namespace TweetsAndNewsOnTelegrm.Controllers.ChatGpt;

[ApiController]
[Route("api/[controller]")]
public class ChatGptController : Controller
{
    private readonly IChatGptService _chatGptService;
    
    public ChatGptController(IChatGptService chatGptService)
    {
        _chatGptService = chatGptService;
    }
    
    [HttpPost("GetNewsSummary")]
    public async Task<IActionResult> GetNewsSummary(string newsText)
    {
        var summary = await _chatGptService.GetNewsSummaryAsync(newsText);
        return Ok(summary);
    }
}