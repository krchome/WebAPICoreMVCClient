using Microsoft.AspNetCore.Mvc;

namespace APIConsume.Controllers
{
    public class StartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}