using System.Linq;
using SharikiApp.Core.Models;

namespace SharikiApp.Models
{
    public interface INewsRepository
    {
        IQueryable<News> GetNews();
    }
}