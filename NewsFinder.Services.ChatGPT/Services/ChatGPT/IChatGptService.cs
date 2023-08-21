namespace NewsFinder.Services.ChatGPT.Services.ChatGPT;

public interface IChatGptService
{
    Task<string> GetNewsSummaryAsync(string newsText);
}