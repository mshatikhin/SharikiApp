using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SharikiApp.Core.Models;
using SharikiApp.Models;
using SharikiApp.Models.Cache;
using SharikiApp.Models.Users;

namespace SharikiApp.Controllers
{
    [Authorize(Users = "admin")]
    public class NewsController : Controller
    {
        private hellraz5_sharikiEntities db = new hellraz5_sharikiEntities();
        private readonly INewsRepository newsRepository;
        private readonly IUserRepository userRepository;
        private readonly IDataCache dataCache;

        public NewsController(
            INewsRepository newsRepository,
            IUserRepository userRepository,
            IDataCache dataCache)
        {
            this.newsRepository = newsRepository;
            this.userRepository = userRepository;
            this.dataCache = dataCache;
        }

        public ActionResult Index()
        {
            var news = newsRepository
                .GetNews()
                .OrderByDescending(n => n.NewsId);
            return View(news.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Login");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(News news)
        {
            if (ModelState.IsValid)
            {
                var user = userRepository.GetUserByLogin(User.Identity.Name);
                if (user != null)
                {
                    news.UserId = user.UserId;
                    db.News.Add(news);
                    db.SaveChanges();
                    dataCache.ReloadNews();
                }
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "Login", news.UserId);
            return View(news);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Login", news.UserId);
            return View(news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(News news)
        {
            if (ModelState.IsValid)
            {
                var user = userRepository.GetUserByLogin(User.Identity.Name);
                if (user != null)
                {
                    news.UserId = user.UserId;
                    db.Entry(news).State = EntityState.Modified;
                    db.SaveChanges();
                    dataCache.ReloadNews();
                }
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Login", news.UserId);
            return View(news);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var news = db.News.Find(id);
            db.News.Remove(news);
            db.SaveChanges();
            dataCache.ReloadNews();
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
