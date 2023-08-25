namespace NewsFinder.Services.ChatGPT.Models.News;

public class OpenAiResponse
{
    public string Summary { get; set; }
    public int Importance { get; set; }
    public string Impact { get; set; }
    public string Coin { get; set; }
}