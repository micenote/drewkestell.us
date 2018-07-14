using DrewKestellSite.Data;
using DrewKestellSite.FormObjects;
using DrewKestellSite.Models;
using Ganss.XSS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace DrewKestellSite.Controllers.Article
{
    public class CommentsController : Controller
    {
        readonly ApplicationContext context;
        readonly HtmlSanitizer sanitizer;

        public CommentsController(ApplicationContext context, HtmlSanitizer sanitizer)
        {
            this.context = context;
            this.sanitizer = sanitizer;
        }

        [HttpPost("/Article/{articleId}/Comments")]
        public async Task<JsonResult> Create(int articleId, ArticleCommentsFormObject formObject)
        {
            if (string.IsNullOrWhiteSpace(formObject.Name))
                throw new Exception("Name is required.");
            if (string.IsNullOrWhiteSpace(formObject.Message))
                throw new Exception("Message is required.");

            await context.ArticleComments.AddAsync(new ArticleComment
            {
                ArticleId = articleId,
                Name = sanitizer.Sanitize(formObject.Name),
                Message = sanitizer.Sanitize(formObject.Message),
                Status = "new",
                DateCreated = DateTime.Now
            });
            await context.SaveChangesAsync();

            return Json(HttpStatusCode.OK);
        }
    }
}
