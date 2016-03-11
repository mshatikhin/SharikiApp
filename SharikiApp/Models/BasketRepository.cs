using System.Data.Entity;
using System.Linq;
using SharikiApp.Core.Models;

namespace SharikiApp.Models
{
    public class BasketRepository : IBasketRepository
    {
        public Basket Add(Basket basket)
        {
            using (var db = new hellraz5_sharikiEntities())
            {
                db.Entry(basket).State = EntityState.Added;
                db.SaveChanges();
                return basket;
            }
        }

        public IQueryable<Basket> GetBaskets()
        {
            var db = new hellraz5_sharikiEntities();
            return db.Baskets;
        }
    }
}