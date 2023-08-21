using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NewsFinder.Services.ChatGPT.Services.ChatGPT;

public class ChatGptService : IChatGptService
{
    private readonly ChatGptSettings _chatGptSettings;
    private readonly HttpClient _httpClient;
    
    public ChatGptService(HttpClient httpClient ,IOptions<ChatGptSettings> chatGptSettings)
    {
        _httpClient = httpClient;
        _chatGptSettings = chatGptSettings.Value;
    }

    public async Task<string> GetNewsSummaryAsync(string newsText)
    {
        var formattedText =
            "Summarize the news below in 10-15 words maximum which is about the financial market. " +
            "At the end, out of 10, tell me how important the news is. " +
            "Specifically, mention which coin(s) the news have impact on, tell me out of 10 how impactful that news is. " +
            "Also, tell me if the news has a negative or positive impact on price of coin(s). Here is the news:" + newsText;

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
        
            return content.Trim();
        }
    }

}