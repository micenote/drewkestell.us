using DrewKestellSite.Models;

namespace DrewKestellSite.ViewModels
{
    public class ArticleCommentViewModel
    {
        public ArticleCommentViewModel(ArticleComment comment)
        {
            Name = comment.Name;
            Message = comment.Message;
            Response = comment.Response;
            DateCreated = comment.DateCreated.ToString();
        }
        
        public string Name { get; }

        public string Message { get; }

        public string Response { get; }

        public string DateCreated { get; }
    }
}
