namespace NewsFinder.Services.TelegramAPI.Services.News.Templates;

public class MessageTemplate
{
    private static string NewsTemplate(bool isUrgent = false)
    {
        return $"{(isUrgent ? "*IMPORTANT NEWS*" : "")}\n\n" +
               "Summary: {Summary}\n\n" +
               "Importance: {Importance}/10 \n" +
               "{Impact} impact on #{Coin}\n " +
               "Submitted by: {Submitter} on {Platform}";
    }

    public static string FormatNewsMessage(
        string impact,
        string coin,
        string summary,
        string importance,
        bool isUrgent = false)
    {
        return NewsTemplate(isUrgent)
            .Replace("{Impact}", impact)
            .Replace("{Coin}", coin)
            .Replace("{Summary}", summary)
            .Replace("{Importance}", importance)
            .Replace("{Submitter}", "Test Submitter")
            .Replace("{Platform}", "Twitter");
    }
}
    