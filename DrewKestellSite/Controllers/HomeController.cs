using DrewKestellSite.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DrewKestellSite.Controllers
{
    public class HomeController : Controller
    {
        readonly ApplicationContext context;

        public HomeController(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var article = await context.Articles
                .FirstOrDefaultAsync();

            return RedirectToAction(
                "Show", 
                "Chapter",
                new { ArticleID = article.Id, ChapterNumber = 1 });
        }
    }
}
