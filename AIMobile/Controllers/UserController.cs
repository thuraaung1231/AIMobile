using Microsoft.AspNetCore.Mvc;

namespace AIMobile.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
