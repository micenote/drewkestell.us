using System.Threading.Tasks;

namespace DrewKestellSite.Concerns
{
    public interface IAuthentication
    {
        Task<int> AuthenticateUser(string username, string password);
    }
}
