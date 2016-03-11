using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using SharikiApp.Core.Models;
using SharikiApp.Models;
using SharikiApp.Models.Cache;

namespace SharikiApp.Controllers
{
    public class BalloonsController : Controller
    {
        private static readonly string VirtualPathRootName = ConfigurationManager.AppSettings["VirtualPathRootName"];
        private readonly IBalloonRepository balloonRepository;
        private readonly IBalloonProvider balloonProvider;
        private readonly IBalloonService balloonService;
        private readonly IDataCache dataCache;

        public BalloonsController(
            IBalloonRepository balloonRepository,
            IDataCache dataCache,
            IBalloonProvider balloonProvider,
            IBalloonService balloonService)
        {
            this.balloonRepository = balloonRepository;
            this.dataCache = dataCache;
            this.balloonProvider = balloonProvider;
            this.balloonService = balloonService;
        }

        public ActionResult Balloon(int id)
        {
            var balloon = balloonRepository.GetBalloonById(id);
            var balloonTypes = balloonProvider.GetBalloonsTypes();
            var balloonModel = new BalloonModel
            {
                Header = balloon.Header,
                Text = balloon.Text,
                Balloon = balloon,
                BalloonTypes = balloonTypes
            };
            ViewBag.Path = VirtualPathRootName;
            ViewBag.Type = "article";
            ViewBag.Title = (balloonModel.Balloon.SeoTitle ?? balloonModel.Balloon.Name) + " (шарики екатеринбург) творческая мастерская Дядя Женя";
            ViewBag.ImageUrl = "http://sharikekb.ru/" + @ViewBag.Path + "/" + balloonModel.Balloon.BalloonImage;
            ViewBag.Description = balloonModel.Balloon.SeoDescription ?? balloonModel.Balloon.Description;
            if (balloonModel.Balloon.SeoKeywords != null)
            {
                ViewBag.Keywords = balloonModel.Balloon.SeoKeywords;
            }
            ViewBag.Url = "http://sharikekb.ru/balloon/" + balloonModel.Balloon.BalloonId;
            if (string.IsNullOrEmpty(balloonModel.Header))
            {
                balloonModel.Header = balloon.Name;
            }
            if (string.IsNullOrEmpty(balloonModel.Text))
            {
                balloonModel.Text =
                    "<p>Воздушные шары ассоциируются у каждого человека с праздничным настроением, радостными эмоциями и торжественными событиями, детством, беззаботностью и романтикой. Их используют для украшения комнат и залов, в качестве подарков на любое торжество. Без шариков невозможно представить детский день рождения, презентацию новой компании, праздник последнего звонка или свадьбу.<p>" +
                    "<p>Наша компания предлагает широкий выбор шаров разных видов по доступной цене.У нас можно заказать:<p>" +
                    "<ul><li>воздушные шары с надписями и логотипами;</li>" +
                    "<li>яркие и красочные милары (шары из фольги);</li>" +
                    "<li>оригинальные цветы и букеты из шаров;</li>" +
                    "<li>фигуры сказочных героев из шаров;</li>" +
                    "<li>облака из сердец и цветов;</li>" +
                    "<li>поздравительные гирлянды и надписи и другую продукцию для наших клиентов.</li></ul>" +
                    "<p>Мы готовы предложить товары из каталога, а также изготовить оригинальные шары на заказ, украсить ими любое помещение, предоставить арки, гирлянды, букеты и другие композиции на любое торжество.<p>" +
                    "<p>Но на этом преимущества, которые получают наши клиенты от сотрудничества, не заканчиваются, ведь мы предлагаем заказать воздушные шары с доставкой по любому адресу в Екатеринбурге.<p>" +
                    "<p>Наши специалисты помогут выбрать необходимую продукцию, поделятся с вами оригинальными идеями относительно оформления торжественных мероприятий и с удовольствием прислушаются ко всем пожеланиям.Мы ценим каждого клиента, поэтому предлагаем лучшее соотношение цены шариков, а также прочих товаров, и их качества.<p>" +
                    "<p>Чтобы заказать шары, достаточно набрать номер телефона нашей компании в удобное для вас время.Оформить заявку можно на нашем сайте.<p>" +
                    "<p>Заказывайте шары отличного качества, любых размеров и цветов на нашей странице в любое время суток!Все заявки мы обрабатываем не только очень оперативно, но и внимательно.<p>";
            }
            return View(balloonModel);
        }

        [Authorize(Users = "admin")]
        public ActionResult Index(string searchText, int? balloonTypeId)
        {
            var balloons = balloonRepository.GetBalloons(null);
            if (balloonTypeId.HasValue && balloonTypeId.Value != -1)
            {
                balloons = balloons.Where(b => b.BalloonTypeId == balloonTypeId);
            }
            if (!string.IsNullOrEmpty(searchText))
            {
                searchText = searchText.ToLower();
                balloons = balloons
                    .Where(b => b.Name.Contains(searchText) ||
                                b.Description != null && b.Description.Contains(searchText) ||
                                b.AdditionalDescription != null &&
                                b.AdditionalDescription.Contains(searchText)
                    );
            }
            ViewBag.Path = VirtualPathRootName;
            var items = new List<BalloonType> { new BalloonType { Name = "Все", BalloonTypeId = -1 } };
            items.AddRange(balloonProvider.GetBalloonsTypes().OrderBy(b => b.Name));
            ViewBag.BalloonTypeId = new SelectList(items, "BalloonTypeId", "Name", balloonTypeId);
            return View(balloons.ToList());
        }

        [Authorize(Users = "admin")]
        public ActionResult Create()
        {
            ViewBag.BalloonTypeId = new SelectList(balloonProvider.GetBalloonsTypes(), "BalloonTypeId", "Name");
            return View();
        }

        [Authorize(Users = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Balloon balloon, HttpPostedFileBase[] uploadImages)
        {
            if (ModelState.IsValid)
            {
                var savedBalloon = balloonRepository.Add(balloon);
                if (uploadImages.Length > 0)
                {
                    foreach (var image in uploadImages)
                    {
                        SaveImagesToFile(savedBalloon, image);
                    }
                }
                dataCache.ReloadBalloons();
                return RedirectToAction("Index");
            }
            ViewBag.BalloonTypeId = new SelectList(balloonProvider.GetBalloonsTypes(), "BalloonTypeId", "Name", balloon.BalloonTypeId);
            return View(balloon);
        }

        [Authorize(Users = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var balloon = balloonRepository.GetBalloonById(id.Value);
            if (balloon == null)
            {
                return HttpNotFound();
            }
            ViewBag.Path = VirtualPathRootName;
            ViewBag.BalloonTypeId = new SelectList(balloonProvider.GetBalloonsTypes(), "BalloonTypeId", "Name", balloon.BalloonTypeId);
            return View(balloon);
        }

        [Authorize(Users = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Balloon balloon, HttpPostedFileBase[] uploadImages)
        {
            if (ModelState.IsValid)
            {
                if (uploadImages.Length > 0)
                {
                    foreach (var image in uploadImages.Where(image => image != null).Where(image => image.ContentLength != 0))
                    {
                        SaveImagesToFile(balloon, image);
                    }
                }
                balloonRepository.SaveBalloon(balloon);
                dataCache.ReloadBalloons();
                return RedirectToAction("Index");
            }
            ViewBag.BalloonTypeId = new SelectList(balloonProvider.GetBalloonsTypes(), "BalloonTypeId", "Name", balloon.BalloonTypeId);
            return View(balloon);
        }

        [Authorize(Users = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var balloon = balloonRepository.GetBalloonById(id.Value);
            if (balloon == null)
            {
                return HttpNotFound();
            }
            ViewBag.Path = VirtualPathRootName;
            return View(balloon);
        }

        [Authorize(Users = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            balloonRepository.Remove(id);
            dataCache.ReloadBalloons();
            return RedirectToAction("Index");
        }

        [Authorize(Users = "admin")]
        private void SaveImagesToFile(Balloon balloon, HttpPostedFileBase image)
        {
            var mapPath = Server.MapPath("~/" + VirtualPathRootName);
            var savedPic = balloonService.SaveImage(balloon, mapPath, image);
            if (savedPic != null)
            {
                balloonRepository.SaveBalloonImage(new[] { savedPic });
            }
        }
    }
}
