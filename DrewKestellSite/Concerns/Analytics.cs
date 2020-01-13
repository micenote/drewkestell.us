using DrewKestellSite.Data;
using DrewKestellSite.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DrewKestellSite.Concerns
{
    public class Analytics : IAnalytics
    {
        readonly ApplicationContext context;

        public Analytics(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task LogRequest(HttpContext httpContext)
        {
            var siteVisit = new SiteVisit
            {
                Url = httpContext.Request.Path,
                IPAddress = httpContext.Connection.RemoteIpAddress.ToString()
            };
            context.SiteVisits.Add(siteVisit);
            await context.SaveChangesAsync();
        }
    }
}
