using System.Linq;
using SharikiApp.Core.Models;

namespace SharikiApp.Models
{
    public interface IArticleRepository
    {
        IQueryable<Article> GetArticles();
    }
}