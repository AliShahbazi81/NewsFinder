using Microsoft.AspNetCore.Mvc;
using NewsFinder.Services.TelegramAPI.Services.News;
using Telegram.Bot;

namespace TweetsAndNewsOnTelegrm.Controllers.Telegram;

[ApiController]
[Route("api/[controller]")]
public class TelegramController : Controller
{
    private readonly IPostInNews _postInNews;

    public TelegramController(IPostInNews postInNews)
    {
        _postInNews = postInNews;
    }

    // [HttpPost("SendTestMessage")]
    // public async Task<IActionResult> SendMessage()
    // {
    //     await _telegramNewsChannel.SendMessageAsync();
    //     return Ok();
    // }
}