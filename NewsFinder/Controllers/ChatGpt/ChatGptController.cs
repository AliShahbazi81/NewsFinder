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
        [FromServices] IPostInNews postIn = null)
    {
        try
        {
            var summary = await _chatGptService.GetNewsSummaryAsync(newsText);
            await postIn.SendMessageAsync(summary);
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
        [FromServices] IPostInNews postIn = null)
    {
        try
        {
            var summary = await _chatGptService.GetNewsSummaryAsync(newsText);
            await postIn.SendPinMessageAsync(summary);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}