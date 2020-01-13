using DrewKestellSite.Concerns;
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
        readonly IAnalytics analytics;

        public ChapterController(ApplicationContext context, IAnalytics analytics)
        {
            this.context = context;
            this.analytics = analytics;
        }

        [HttpGet("/Article/{articleId}/Chapter/{chapterNumber}")]
        public async Task<IActionResult> Show(int articleId, int chapterNumber)
        {
            await analytics.LogRequest(HttpContext);

            var chapter = await context.ArticleChapters
                .Include(c => c.Article.Comments)
                .Include(c => c.Article.ArticleChapters)
                .FirstOrDefaultAsync(c => c.ArticleId == articleId && c.ChapterNumber == chapterNumber);

            return View("~/Views/Article/Chapter/Show.cshtml", new ArticleChapterViewModel(chapter));
        }
    }
}
