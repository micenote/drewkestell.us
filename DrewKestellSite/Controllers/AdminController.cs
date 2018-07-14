using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrewKestellSite.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public IActionResult Index() => 
            View("~/Views/Admin/Index.cshtml");
    }
}
