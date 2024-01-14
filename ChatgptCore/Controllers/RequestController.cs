using ChatgptCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using System.Net;
using Newtonsoft.Json.Linq;


namespace ChatgptCore.Controllers
{
    public class RequestController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> SendAsync([FromBody] RequestGptModel requestGptModel)
        {
            if (requestGptModel.Message == null)
            {
                return new BadRequestResult();
            }
            ChatGPT chatGPT = new ChatGPT();
            string? response = await chatGPT.SendRequest(requestGptModel.Message);

            if (response == null)
            {
                ErrorBadRequestModel badRequest = new ErrorBadRequestModel("ChatGPT returned null");
                return new BadRequestObjectResult(badRequest);
            }

            return new OkResult();
        }
    }
}