using System.Data.Entity;
using System.Linq;
using System.Web.UI.WebControls.Expressions;
using SharikiApp.Core.Models;

namespace SharikiApp.Models
{
    public class BalloonRepository : IBalloonRepository
    {
        public Balloon Add(Balloon balloon)
        {
            using (var db = new hellraz5_sharikiEntities())
            {
                db.Balloons.Add(balloon);
                db.SaveChanges();
                return balloon;
            }
        }

        public void Remove(int id)
        {
            using (var db = new hellraz5_sharikiEntities())
            {
                var balloon = db.Balloons.Find(id);
                db.Balloons.Remove(balloon);
                db.SaveChanges();
            }
        }

        public IQueryable<Balloon> GetBalloons(int? balloonType)
        {
            var db = new hellraz5_sharikiEntities();
            if (balloonType.HasValue)
            {
                return db.Balloons
                    .Include("BalloonType")
                    .Where(b => !b.Hide)
                    .Where(b => b.BalloonTypeId == balloonType.Value)
                    .OrderByDescending(b => b.Ordering)
                    .ThenBy(o => o.BalloonTypeId);
            }
            return db.Balloons.OrderByDescending(b => b.Ordering);
        }

        public void SaveBalloon(Balloon balloon)
        {
            using (var db = new hellraz5_sharikiEntities())
            {
                var balloonFromDb = db.Balloons.Single(b => b.BalloonId == balloon.BalloonId);
                balloonFromDb.BalloonType = balloon.BalloonType;
                balloonFromDb.Description = balloon.Description;
                balloonFromDb.AdditionalDescription = balloon.AdditionalDescription;
                balloonFromDb.Hide = balloon.Hide;
                balloonFromDb.Name = balloon.Name;
                balloonFromDb.Price = balloon.Price;
                balloonFromDb.Ordering = balloon.Ordering;
                balloonFromDb.DiscountDescription = balloon.DiscountDescription;
                balloonFromDb.Discount = balloon.Discount;
                balloonFromDb.Header = balloon.Header;
                balloonFromDb.Text = balloon.Text;

                balloonFromDb.SeoTitle = balloon.SeoTitle;
                balloonFromDb.SeoKeywords = balloon.SeoKeywords;
                balloonFromDb.SeoDescription = balloon.SeoDescription;

                db.Entry(balloonFromDb).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void SaveBalloonImage(Balloon[] balloons)
        {
            var ids = balloons.Select(b => b.BalloonId).ToArray();
            using (var db = new hellraz5_sharikiEntities())
            {
                var balloonsFromDb = db.Balloons.Where(b => ids.Contains(b.BalloonId)).ToArray();
                if (balloonsFromDb.Length > 0)
                {
                    foreach (var balloon in balloonsFromDb)
                    {
                        balloon.BalloonImage = balloons.Single(b => b.BalloonId == balloon.BalloonId).BalloonImage;
                        db.Entry(balloon).State = EntityState.Modified;
                    }
                    db.SaveChanges();
                }
            }
        }

        public Balloon GetBalloonById(int balloonId)
        {
            using (var db = new hellraz5_sharikiEntities())
            {
                return db.Balloons
                    .Include(b=>b.BalloonType)
                    .SingleOrDefault(b => b.BalloonId == balloonId);
            }
        }

        public IQueryable<BalloonType> GetBalloonsTypes()
        {
            var db = new hellraz5_sharikiEntities();
            return db.BalloonTypes.Include("Balloons").OrderBy(b => b.Order);
        }
    }
}