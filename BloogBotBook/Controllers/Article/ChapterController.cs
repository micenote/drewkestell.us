using BloogBotBook.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BloogBotBook.Controllers.Article
{
    public class ChapterController : Controller
    {
        readonly ApplicationContext context;

        public ChapterController(ApplicationContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("/Article/{articleId}/Chapter/{chapterNumber}")]
        public async Task<IActionResult> Show(int articleId, int chapterNumber)
        {
            var chapter = await context.ArticleChapters
                .FirstOrDefaultAsync(c => c.ArticleId == articleId && c.Value == chapterNumber);

            return View("~/Views/Article/Chapter/Show.cshtml", chapter);
        }
    }
}
