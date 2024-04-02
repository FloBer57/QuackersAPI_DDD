using Microsoft.AspNetCore.Mvc;

namespace QuackersAPI_DDD.API.Controller
{
    public class PersonRoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
