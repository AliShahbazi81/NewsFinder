using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace NewsFinder.Services.TelegramAPI.Services.News.GetPosts;

public class GetNews : IGetNews
{
    private readonly TelegramBotClient _botClient;

    public GetNews(IOptions<TelegramSendingOptions> options)
    {
        _botClient = new TelegramBotClient(options.Value.API_TOKEN);
    }
}