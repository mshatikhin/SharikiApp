using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SharikiApp.Core.Models;
using SharikiApp.Models.Cache;

namespace SharikiApp.Controllers
{
    [Authorize(Users = "admin")]
    public class BalloonTypeController : Controller
    {
        private hellraz5_sharikiEntities db = new hellraz5_sharikiEntities();
        private readonly IDataCache dataCache;

        public BalloonTypeController(IDataCache dataCache)
        {
            this.dataCache = dataCache;
        }

        public ActionResult Index()
        {
            return View(db.BalloonTypes.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BalloonType balloonType)
        {
            if (ModelState.IsValid)
            {
                db.BalloonTypes.Add(balloonType);
                db.SaveChanges();
                dataCache.ReloadTypes();
                return RedirectToAction("Index");
            }

            return View(balloonType);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BalloonType balloonType = db.BalloonTypes.Find(id);
            if (balloonType == null)
            {
                return HttpNotFound();
            }
            return View(balloonType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BalloonType balloonType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(balloonType).State = EntityState.Modified;
                db.SaveChanges();
                dataCache.ReloadTypes();
                return RedirectToAction("Index");
            }
            return View(balloonType);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BalloonType balloonType = db.BalloonTypes.Find(id);
            if (balloonType == null)
            {
                return HttpNotFound();
            }
            return View(balloonType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var balloonType = db.BalloonTypes.Find(id);
            db.BalloonTypes.Remove(balloonType);
            db.SaveChanges();
            dataCache.ReloadTypes();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
