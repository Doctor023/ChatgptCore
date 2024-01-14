using ChatgptCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using System.Net;


namespace ChatgptCore.Controllers
{
    public class RequestController : Controller
    {
        [HttpPost]
        public async Task<HttpResponseMessage> SendAsync([FromBody] RequestGptModel requestGptModel)
        {
            if (requestGptModel.Message == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Message can't be empty")
                };
            }
            ChatGPT chatGPT = new ChatGPT();
            string? response = await chatGPT.SendRequest(requestGptModel.Message);

            if (response == null)
            {
                var errorResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Вот такие пироги")
                };

                return errorResponse;
            }

            return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(response) };
        }
    }
}