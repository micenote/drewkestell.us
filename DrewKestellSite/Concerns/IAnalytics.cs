using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DrewKestellSite.Concerns
{
    public interface IAnalytics
    {
        Task LogRequest(HttpContext httpContext);
    }
}
