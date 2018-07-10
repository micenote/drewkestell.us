using Microsoft.AspNetCore.Mvc;

namespace BloogBotBook.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() =>
            RedirectToAction("Show", "Chapter", new { ArticleID = 1, ChapterNumber = 1 });
    }
}
