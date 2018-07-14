using DrewKestellSite.Data;
using DrewKestellSite.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DrewKestellSite.Controllers.Article
{
    public class ChapterController : Controller
    {
        readonly ApplicationContext context;

        public ChapterController(ApplicationContext context)
        {
            this.context = context;
        }

        [HttpGet("/Article/{articleId}/Chapter/{chapterNumber}")]
        public async Task<IActionResult> Show(int articleId, int chapterNumber)
        {
            var chapter = await context.ArticleChapters
                .Include(c => c.Article.Comments)
                .Include(c => c.Article.ArticleChapters)
                .FirstOrDefaultAsync(c => c.ArticleId == articleId && c.ChapterNumber == chapterNumber);

            return View("~/Views/Article/Chapter/Show.cshtml", new ArticleChapterViewModel(chapter));
        }
    }
}
