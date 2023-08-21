namespace NewsFinder.Services.MessagingAPI.Services.SMS.Settings;

public class TwilioSettings
{
    public string AccountSID { get; set; }
    public string AuthToken { get; set; }
    public string To { get; set; }
    public string From { get; set; }
    public string MessagingServiceSid { get; set; }
    
}