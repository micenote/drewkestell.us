using BloogBotBook.Data;
using BloogBotBook.FormObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BloogBotBook.Controllers
{
    [Route("Article/{action=Index}/{articleId?}")]
    public class ArticleController : Controller
    {
        readonly ApplicationContext context;

        public ArticleController(ApplicationContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index() => 
            View(await context.Articles.ToListAsync());

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(ArticleFormObject formObject)
        {
            await context.AddAsync(new Models.Article
            {
                Slug = formObject.Slug
            });
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int articleId) =>
            View(await context.Articles.SingleAsync(a => a.Id == articleId));

        [HttpPost]
        public async Task<IActionResult> Update(int articleId, ArticleFormObject formObject)
        {
            var article = await context.Articles.FirstOrDefaultAsync(a => a.Id == articleId);
            article.Slug = formObject.Slug;
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
