namespace NewsFinder.Services.TelegramAPI.Services.News;

public interface ITelegramNewsChannel
{
    Task<int> SendMessageAsync();
}