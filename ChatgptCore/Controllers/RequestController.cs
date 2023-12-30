using ChatgptCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChatgptCore.Controllers
{
    public class RequestController : Controller
    {
        [HttpPost]
        public IActionResult Send()
        {
            return Json("Отправлено");
        }
    }
}