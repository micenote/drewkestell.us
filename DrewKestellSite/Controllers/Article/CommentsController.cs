using DrewKestellSite.Configuration;
using DrewKestellSite.Data;
using DrewKestellSite.FormObjects;
using DrewKestellSite.Models;
using Ganss.XSS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net;
using System.Threading.Tasks;

namespace DrewKestellSite.Controllers.Article
{
    public class CommentsController : Controller
    {
        readonly ApplicationContext context;
        readonly HtmlSanitizer sanitizer;
        readonly ApiConfiguration config;

        public CommentsController(ApplicationContext context, HtmlSanitizer sanitizer, IOptions<ApiConfiguration> config)
        {
            this.context = context;
            this.sanitizer = sanitizer;
            this.config = config.Value;
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

            var client = new SendGridClient(config.SendGridAPIKey);
            var from = new EmailAddress("admin@drewkestel.us", "Site Admin");
            var to = new EmailAddress("drew.kestell@gmail.com", "Drew Kestell");
            var subject = "A new comment at drewkestell.us is pending your approval.";
            var message = MailHelper.CreateSingleEmail(from, to, subject, formObject.Message, formObject.Message);
            await client.SendEmailAsync(message);

            return Json(HttpStatusCode.OK);
        }
    }
}
