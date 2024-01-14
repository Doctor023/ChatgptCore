using System.Net.Http.Json;
using System.Text.Json.Serialization;

class ChatGPT
{
    public async Task<string?> SendRequest()
    {
        string messageFromVK = "Привет";
        string _promtDenisGPT = "Ты голосовой помощник";
        string apiKey = "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";
        string replyText = "Как ты?";

        string endpoint = "https://api.openai.com/v1/chat/completions";

        List<Message> messages = new List<Message>();

        var httpClient = new HttpClient();
        httpClient.Timeout = TimeSpan.FromMinutes(5);

        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

        while (true)
        {
            var systemMessage = new Message() { Role = "system", Content = _promtDenisGPT };

            Console.Write($"{messageFromVK}");




            if (messageFromVK is not { Length: > 0 }) return null;

            Message message = new Message() { Role = "user", Content = messageFromVK };
            if (replyText == null) { message = new Message() { Role = "user", Content = messageFromVK }; }
            else { message = new Message() { Role = "user", Content = $"{replyText}\n{messageFromVK}" }; }

            messages.Add(message);


            var requestData = new Request()
            {
                ModelId = "gpt-3.5-turbo",
                Messages = messages
            };

            using var response = await httpClient.PostAsJsonAsync(endpoint, requestData);


            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"{(int)response.StatusCode} {response.StatusCode}");
                return null;
            }

            ResponseData? responseData = await response.Content.ReadFromJsonAsync<ResponseData>();
            var responseDataID = responseData?.Id;
            Console.WriteLine($"ID сессии: {responseDataID}");

            var choices = responseData?.Choices ?? new List<Choice>();
            if (choices.Count == 0)
            {
                Console.WriteLine("No choices were returned by the API");
                continue;
            }
            var choice = choices[0];
            var responseMessage = choice.Message;

            messages.Add(responseMessage);
            var responseText = responseMessage.Content.Trim();
            return responseText;
        }
    }
    }