using System.Linq;
using SharikiApp.Core.Models;

namespace SharikiApp.Models
{
    public interface IBasketRepository
    {
        Basket Add(Basket basket);
        IQueryable<Basket> GetBaskets();
    }
}