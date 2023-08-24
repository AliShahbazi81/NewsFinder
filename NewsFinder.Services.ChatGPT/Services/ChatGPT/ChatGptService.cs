using System.Text;
using Microsoft.Extensions.Options;
using NewsFinder.Services.ChatGPT.Models.News;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NewsFinder.Services.ChatGPT.Services.ChatGPT;

public class ChatGptService : IChatGptService
{
    private readonly ChatGptSettings _chatGptSettings;
    private readonly HttpClient _httpClient;

    public ChatGptService(HttpClient httpClient, IOptions<ChatGptSettings> chatGptSettings)
    {
        _httpClient = httpClient;
        _chatGptSettings = chatGptSettings.Value;
    }

    public async Task<OpenAiResponse> GetNewsSummaryAsync(string newsText)
    {
        var formattedText =
            "Summarize the news below in 10-15 words about the financial market and format your response as follows: \n\n" +
            "Summary: [Your Summary Here]\n" +
            "Importance: [Importance out of 10]\n" +
            "Impact: [Positive/Negative Impact]\n" +
            "Coin: [Coin/Stock]\n\n" +
            "News: " + newsText;

        var messages = new List<object>
        {
            new { role = "user", content = formattedText }
        };

        var requestBody = new
        {
            model = _chatGptSettings.OPENAI_API_MODEL,
            messages = messages
        };

        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_chatGptSettings.OPENAI_API_KEY}");
            var response = await httpClient.PostAsync(
                $"{_chatGptSettings.OPENAI_API_ENDPOINT}",
                new StringContent(JsonConvert.SerializeObject(requestBody),
                    Encoding.UTF8,
                    "application/json"));

            if (!response.IsSuccessStatusCode)
                throw new Exception("Unexpected response from OpenAI API");

            var responseBody = await response.Content.ReadAsStringAsync();
            var parsedResponse = JsonConvert.DeserializeObject<JObject>(responseBody);

            var content = parsedResponse?["choices"]?[0]?["message"]?["content"]?.ToString();
            if (string.IsNullOrEmpty(content))
                throw new Exception("Unexpected response format from OpenAI API");

            var lines = content.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            if (lines.Length < 4)
                throw new Exception("Unexpected response format from OpenAI API");

            var summary = lines[0].Replace("Summary: ", "").Trim();
            var importance = lines[1].Replace("Importance: ", "").Trim();
            var impact = lines[2].Replace("Impact: ", "").Trim();
            var coin = lines[3].Replace("Coin: ", "").Trim();

            return new OpenAiResponse
            {
                Summary = summary,
                Importance = importance,
                Impact = impact,
                Coin = coin
            };
        }
    }
}