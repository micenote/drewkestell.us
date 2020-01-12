using DrewKestellSite.Data;
using DrewKestellSite.FormObjects;
using DrewKestellSite.Models;
using DrewKestellSite.ViewModels;
using Ganss.XSS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DrewKestellSite.Controllers.Admin
{
    [Authorize]
    public class ArticleController : Controller
    {
        readonly ApplicationContext context;
        readonly HtmlSanitizer sanitizer;

        public ArticleController(ApplicationContext context, HtmlSanitizer sanitizer)
        {
            this.context = context;
            this.sanitizer = sanitizer;
        }

        [HttpGet("/Admin/Article")]
        public async Task<IActionResult> Index() =>
            View("~/Views/Admin/Article/Index.cshtml", await context.Articles.ToListAsync());

        [HttpGet("/Admin/Article/Create")]
        public IActionResult Create() => View("~/Views/Admin/Article/Create.cshtml");

        [HttpPost("/Admin/Article/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArticleFormObject formObject)
        {
            var article = new Models.Article
            {
                Slug = sanitizer.Sanitize(formObject.Slug),
                Title = sanitizer.Sanitize(formObject.Title),
                Description = sanitizer.Sanitize(formObject.Description),
                ThumbnailUrl = sanitizer.Sanitize(formObject.ThumbnailUrl)
            };
            var index = 0;
            foreach (var chapter in formObject.ArticleChapters)
                article.ArticleChapters.Add(new ArticleChapter
                {
                    Name = sanitizer.Sanitize(chapter.LinkText),
                    Text = sanitizer.Sanitize(chapter.Text),
                    ChapterNumber = ++index
                });
            await context.AddAsync(article);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet("/Admin/Article/Update/{articleId}")]
        public async Task<IActionResult> Update(int articleId) =>
            View("~/Views/Admin/Article/Update.cshtml",
                new ArticleViewModel(
                    await context.Articles.Include(a => a.ArticleChapters)
                        .SingleAsync(a => a.Id == articleId))
                );

        [HttpPost("/Admin/Article/Update/{articleId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int articleId, ArticleFormObject formObject)
        {
            var article = await context.Articles.Include(a => a.ArticleChapters).FirstOrDefaultAsync(a => a.Id == articleId);
            article.Slug = sanitizer.Sanitize(formObject.Slug);
            article.Title = sanitizer.Sanitize(formObject.Title);
            article.Description = sanitizer.Sanitize(formObject.Description);
            article.ThumbnailUrl = sanitizer.Sanitize(formObject.ThumbnailUrl);
            article.ArticleChapters.Clear();
            var index = 0;
            foreach (var chapter in formObject.ArticleChapters)
                article.ArticleChapters.Add(new Models.ArticleChapter
                {
                    Name = sanitizer.Sanitize(chapter.LinkText),
                    Text = chapter.Text,
                    ChapterNumber = ++index
                });
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // this should be HttpDelete
        [HttpGet("/Admin/Article/Delete/{articleId}")]
        public async Task<IActionResult> Delete(int articleId)
        {
            context.Articles.Remove(await context.Articles.FirstOrDefaultAsync(a => a.Id == articleId));
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
