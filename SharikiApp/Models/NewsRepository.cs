using System.Linq;
using SharikiApp.Core.Models;

namespace SharikiApp.Models
{
    public class NewsRepository : INewsRepository
    {
        public IQueryable<News> GetNews()
        {
            var db = new hellraz5_sharikiEntities();
            return db.News.Include("User");
        }
    }
}