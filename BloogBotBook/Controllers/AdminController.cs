using Microsoft.AspNetCore.Mvc;

namespace BloogBotBook.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index() => View();
    }
}
