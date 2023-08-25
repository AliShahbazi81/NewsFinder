using Microsoft.AspNetCore.Mvc;
using NewsFinder.Services.ChatGPT.Services.ChatGPT;
using NewsFinder.Services.TelegramAPI.Services.News;

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
    public async Task<IActionResult> GetNewsSummary(
        string newsText,
        [FromServices] ITelegramNewsChannel telegram = null)
    {
        try
        {
            var summary = await _chatGptService.GetNewsSummaryAsync(newsText);
            await telegram.SendMessageAsync(summary);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost("GetNewsSummaryPin")]
    public async Task<IActionResult> GetNewsSummaryPin(
        string newsText,
        [FromServices] ITelegramNewsChannel telegram = null)
    {
        try
        {
            var summary = await _chatGptService.GetNewsSummaryAsync(newsText);
            await telegram.SendPinMessageAsync(summary);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}