namespace NewsFinder.Services.TelegramAPI.Services.News.Templates;

public class MessageTemplate
{
    private static string NewsTemplate(bool isUrgent = false)
    {
        return $"{(isUrgent ? "*IMPORTANT NEWS*" : "")}\n" +
               "{Impact} impact on {Coin}\n\n +" +
               "Summary: {News}\n" +
               "Link to the news: {Link}\n" +
               "Submitted by: {Submitter} on {Platform}";
    }

    public static string FormatNewsMessage(
        string impact,
        string coin,
        string news,
        string link,
        string submitter,
        string platform,
        bool isUrgent = false)
    {
        return NewsTemplate(isUrgent)
            .Replace("{Impact}", impact)
            .Replace("{Coin}", coin)
            .Replace("{News}", news)
            .Replace("{Link}", link)
            .Replace("{Submitter}", submitter)
            .Replace("{Platform}", platform);
    }
}
    