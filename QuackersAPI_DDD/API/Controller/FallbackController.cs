using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace QuackersAPI_DDD.API.Controller
{
    [AllowAnonymous]
    public class FallbackController : ControllerBase
    {
        public IActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/html");
        }
    }
}
