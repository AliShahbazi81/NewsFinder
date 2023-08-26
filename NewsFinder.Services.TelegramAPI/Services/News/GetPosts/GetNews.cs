using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NewsFinder.DataAccess.Data.DbContext;
using Telegram.Bot;

namespace NewsFinder.Services.TelegramAPI.Services.News.GetPosts;

public class GetNews : IGetNews
{
    private readonly TelegramBotClient _botClient;
    private readonly ApplicationDbContext _context;

    public GetNews(IOptions<TelegramSendingOptions> options, ApplicationDbContext context)
    {
        _botClient = new TelegramBotClient(options.Value.API_TOKEN);
        _context = context;
    }
    public async Task<IEnumerable<string>> GetLatestPostAsync()
    {
        var updates = await _botClient.GetUpdatesAsync();
        var targetChannels = await _context.TelegramSupportedChannels
            .ToListAsync();
        
        var posts = new List<string>();

        foreach(var channel in targetChannels)
        {
            var latestMessage = updates.LastOrDefault(x => 
                x.Message.Chat.Username == channel.UserName && 
                x.Message.Date.ToUniversalTime() > channel.LastTimeChecked.ToUniversalTime());
            
            if (latestMessage != null)
            {
                if (latestMessage.Message?.Text != null) posts.Add(latestMessage.Message?.Text);
                channel.LastTimeChecked = DateTime.UtcNow;
                _context.Update(channel);
            }
        }

        await _context.SaveChangesAsync();

        return posts;
    }
}