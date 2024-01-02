using ChatgptCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChatgptCore.Controllers
{
    public class RequestController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> SendAsync()
        {
            ChatGPT chatGPT = new ChatGPT();
            string? response = await chatGPT.SendRequest();
            return Json(response);
        }
    }
}