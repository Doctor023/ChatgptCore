using System.Net.Http.Json;
using System.Text.Json.Serialization;

class ChatGPT
{
    public async Task<string?> SendRequest()
    {
        // токен из личного кабинета
        string apiKey = "sk-95Ez2Z1rtgegrRTFFDSTVTdsdfsgdv3422kjhLghnh53QiT8F";
        // адрес api для взаимодействия с чат-ботом
        string endpoint = "https://api.openai.com/v1/chat/completions";
        // набор соообщений диалога с чат-ботом
        List<Message> messages = new List<Message>();
        // HttpClient для отправки сообщений
        var httpClient = new HttpClient();
        // устанавливаем отправляемый в запросе токен
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            // ввод сообщения пользователя
            Console.Write("Привет");
            var content = "Привет";

            // если введенное сообщение имеет длину меньше 1 символа
            // то выходим из цикла и завершаем программу
            if (content is not { Length: > 0 }) return null;
            // формируем отправляемое сообщение
            var message = new Message() { Role = "user", Content = content };
            // добавляем сообщение в список сообщений
            messages.Add(message);

            // формируем отправляемые данные
            var requestData = new Request()
            {
                ModelId = "gpt-3.5-turbo",
                Messages = messages
            };
            // отправляем запрос
            using var response = await httpClient.PostAsJsonAsync(endpoint, requestData);

            // если произошла ошибка, выводим сообщение об ошибке на консоль
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"{(int)response.StatusCode} {response.StatusCode}");
                return null;
            }
            // получаем данные ответа
            ResponseData? responseData = await response.Content.ReadFromJsonAsync<ResponseData>();

            var choices = responseData?.Choices ?? new List<Choice>();
            if (choices.Count == 0)
            {
                Console.WriteLine("No choices were returned by the API");
                return null;
            }
            var choice = choices[0];
            var responseMessage = choice.Message;
            // добавляем полученное сообщение в список сообщений
            messages.Add(responseMessage);
            string responseText = responseMessage.Content.Trim();
            return responseText;
        }
    }