using Microsoft.AspNetCore.Mvc;

namespace QuackersAPI_DDD.API.Controller
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
