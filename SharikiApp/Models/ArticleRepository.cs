using System.Linq;
using SharikiApp.Core.Models;

namespace SharikiApp.Models
{
    public class ArticleRepository : IArticleRepository
    {
        public IQueryable<Article> GetArticles()
        {
            var db = new hellraz5_sharikiEntities();
            return db.Articles;
        }
    }
}