namespace NewsFinder.Services.TelegramAPI.Services.News.Templates;

public class MessageTemplate
{
    // private static string NewsTemplate(bool isUrgent = false)
    // {
    //     return $"{(isUrgent ? "*IMPORTANT NEWS*" : "")}\n\n" +
    //            "Summary: {Summary}\n\n" +
    //            "Importance: {Importance}/10 \n" +
    //            "{Impact} impact on #{Coin}\n " +
    //            "Submitted by: {Submitter} on {Platform}";
    // }
    
    private static string NewsTemplate(string impact = "positive", int importance = 0, bool isUrgent = false)
    {
        var impactEmoji = impact.ToLower() == "positive" ? "📈" : "📉";
        var importanceText = importance >= 7 
            ? $"*Importance: {importance}/10*"
            : $"Importance: {importance}/10";

        var centeredNewsAlert = isUrgent ? "🚨          *IMPORTANT NEWS*          🚨" : "";

        return $"{centeredNewsAlert}\n\n" +
               $"📝 Summary: {{Summary}}\n\n" +
               $"❗️ {importanceText} \n" +
               $"{impactEmoji} {impact} impact on #{{Coin}}\n " +
               "📤 Submitted by: {{Submitter}} on {{Platform}}";
    }

    public static string FormatNewsMessage(
        string impact,
        string coin,
        string summary,
        int importance,
        bool isUrgent = false)
    {
        return NewsTemplate(impact, importance, isUrgent)
            .Replace("{Impact}", impact)
            .Replace("{Coin}", coin)
            .Replace("{Summary}", summary)
            .Replace("{Importance}", importance.ToString())
            .Replace("{Submitter}", "Test Submitter")
            .Replace("{Platform}", "Twitter");
    }
}
    