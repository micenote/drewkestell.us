using DrewKestellSite.Models;

namespace DrewKestellSite.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(User user)
        {
            Id = user.Id;
            Username = user.Username;
        }

        public int Id { get; }

        public string Username { get; }
    }
}
