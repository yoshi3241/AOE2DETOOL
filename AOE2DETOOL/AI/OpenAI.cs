using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AOE2DETOOL.AI
{
    public class ChatGPTResponse
    {
        public List<Choice> choices { get; set; }
        public string completion_id { get; set; }
        public string model { get; set; }
    }

    public class Choice
    {
        public float? logprobs { get; set; }
        public string finish_reason { get; set; }
        public string text { get; set; }
    }

    public class OpenAI2
    {
        private readonly HttpClient client;
        private readonly string apiKey;

        public OpenAI2(string apiKey)
        {
            client = new HttpClient();
            this.apiKey = apiKey;
        }

        public async Task<string> ChatGPT(string prompt)
        {
            var url = "https://api.openai.com/v1/engines/davinci-codex/completions";
            var requestBody = new
            {
                prompt = prompt,
                max_tokens = 500,
                n = 1,
                //stop = "\n"
            };
            var requestContent = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json"
            );
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
            var response = await client.PostAsync(url, requestContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;
        }
    }
}
