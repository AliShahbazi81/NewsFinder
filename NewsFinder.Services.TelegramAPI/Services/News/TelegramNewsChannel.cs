using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace NewsFinder.Services.TelegramAPI.Services.News;

public class TelegramNewsChannel : ITelegramNewsChannel
{
    private readonly ITelegramBotClient _botClient;
    private readonly string _channelName;

    public TelegramNewsChannel(ITelegramBotClient botClient, IOptions<TelegramSendingOptions> options)
    {
        _botClient = botClient;
        _channelName = options.Value.NewsChannel;
    }

    public async Task<int> SendMessageAsync()
    {
        var message = await _botClient.SendTextMessageAsync(_channelName, "Test Message", ParseMode.Markdown);
        return message.MessageId;
    }
}