using NewsFinder.Services.ChatGPT.Models.News;

namespace NewsFinder.Services.TelegramAPI.Services.News;

public interface ITelegramNewsChannel
{
    Task<int> SendMessageAsync(OpenAiResponse receivedNews);
    Task<int> SendPinMessageAsync(OpenAiResponse receivedNews);
}