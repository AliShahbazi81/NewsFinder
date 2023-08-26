namespace NewsFinder.Services.TelegramAPI.Services.News.GetPosts;

public interface IGetNews
{
    Task<IEnumerable<string>> GetLatestPostAsync();
}