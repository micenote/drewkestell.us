using DrewKestellSite.Concerns;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DrewKestellSite.Controllers.Admin
{
    public class SessionController : Controller
    {
        readonly IAuthentication authentication;

        public SessionController(IAuthentication authentication)
        {
            this.authentication = authentication;
        }

        [HttpGet("/Admin/Session/Create")]
        public IActionResult Create() => View("~/Views/Admin/Session/Create.cshtml");

        [HttpPost("/Admin/Session/Create")]
        public async Task<IActionResult> Create(string username, string password)
        {
            var userId = await authentication.AuthenticateUser(username, password);

            await SignInAsync(HttpContext, userId);

            return RedirectToAction("Index", "Admin");
        }

        // this should be HttpDestroy
        [HttpGet("/Admin/Session/Delete")]
        public async Task<IActionResult> Delete()
        {
            await SignOutAsync(HttpContext);
            return RedirectToAction("Index", "Home");
        }

        [NonAction]
        async Task SignInAsync(HttpContext httpContext, int userId) =>
            await httpContext.SignInAsync("KestellSiteAuthenticationScheme", BuildClaimsPrincipal(userId));

        [NonAction]
        async Task SignOutAsync(HttpContext httpContext) =>
            await httpContext.SignOutAsync("KestellSiteAuthenticationScheme");

        static ClaimsPrincipal BuildClaimsPrincipal(int userId)
        {
            var claimsIdentity = new ClaimsIdentity("KestellSiteAuthenticationScheme");
            claimsIdentity.AddClaim(new Claim("UserId", userId.ToString()));
            return new ClaimsPrincipal(claimsIdentity);
        }
    }
}
