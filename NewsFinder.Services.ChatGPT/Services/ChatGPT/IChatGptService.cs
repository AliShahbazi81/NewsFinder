using NewsFinder.Services.ChatGPT.Models.News;

namespace NewsFinder.Services.ChatGPT.Services.ChatGPT;

public interface IChatGptService
{
    Task<OpenAiResponse> GetNewsSummaryAsync(string newsText);
}