using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using SharikiApp.Core.Models;
using SharikiApp.Models;
using SharikiApp.Models.Mailer;

namespace SharikiApp.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBalloonRepository balloonRepository;
        private readonly IBasketRepository basketRepository;
        private readonly IMailer mailer;
        private static readonly string VirtualPathRootName = ConfigurationManager.AppSettings["VirtualPathRootName"];

        public BasketController(
            IBalloonRepository balloonRepository,
            IBasketRepository basketRepository, IMailer mailer)
        {
            this.balloonRepository = balloonRepository;
            this.basketRepository = basketRepository;
            this.mailer = mailer;
        }

        [HttpPost]
        public decimal GetPriceWithDiscount(int balloonId, int count)
        {
            var b = (BasketM)Session["basket"];
            if (b != null)
            {
                if (b.Goods != null)
                {
                    var balloonInBasket = b.Goods.FirstOrDefault(g => g.Balloon.BalloonId == balloonId);
                    if (balloonInBasket != null)
                        return balloonInBasket.PriceWithDiscount;
                }
            }
            return 0;
        }

        [HttpPost]
        public string ChangeCount(int balloonId, int count)
        {
            var b = (BasketM)Session["basket"];
            if (b != null)
            {
                if (b.Goods == null)
                {
                    b.Goods = new List<Good>();
                }
                var balloonInBasket = b.Goods.FirstOrDefault(g => g.Balloon.BalloonId == balloonId);
                if (balloonInBasket == null)
                {
                    var balloon = balloonRepository.GetBalloonById(balloonId);
                    b.Goods.Add(new Good
                    {
                        Balloon = balloon,
                        Count = count
                    });
                }
                else
                {
                    balloonInBasket.Count = count;
                }
            }
            else
            {
                var balloon = balloonRepository.GetBalloonById(balloonId);
                var basket = new BasketM
                {
                    BasketId = Guid.NewGuid(),
                    Goods = new List<Good>
                    {
                        new Good
                        {
                            Balloon = balloon,
                            Count = count
                        }
                    }
                };
                Session.Add("basket", basket);
            }
            var result = GetBasketInfo();
            return result;
        }

        [HttpPost]
        public string AddToBasket(int balloonId, int count)
        {
            var b = (BasketM)Session["basket"];
            if (b != null)
            {
                if (b.Goods == null)
                {
                    b.Goods = new List<Good>();
                }
                var balloonInBasket = b.Goods.FirstOrDefault(g => g.Balloon.BalloonId == balloonId);
                if (balloonInBasket == null)
                {
                    var balloon = balloonRepository.GetBalloonById(balloonId);
                    b.Goods.Add(new Good
                    {
                        Balloon = balloon,
                        Count = count
                    });
                }
                else
                {
                    balloonInBasket.Count = balloonInBasket.Count + count;
                }
            }
            else
            {
                var balloon = balloonRepository.GetBalloonById(balloonId);
                var basket = new BasketM
                {
                    BasketId = Guid.NewGuid(),
                    Goods = new List<Good>
                    {
                        new Good
                        {
                            Balloon = balloon,
                            Count = count
                        }
                    }
                };
                Session.Add("basket", basket);
            }
            var result = GetBasketInfo();
            return result;
        }

        public string GetBasketInfo()
        {
            var b = (BasketM)Session["basket"];
            if (b != null)
            {
                var sum = b.Goods
                    .Where(good => good.Balloon.Price.HasValue)
                    .Sum(good => good.Count * good.PriceWithDiscount);

                var result = b.Goods.Count() + " товар(а/ов) на " + sum;
                return result;
            }
            return "В корзине нет товаров";
        }

        public ActionResult Basket()
        {
            ViewBag.Title = "Корзина (шарики екатеринбург) творческая мастерская Дядя Женя";
            ViewBag.Type = "article";
            ViewBag.ImageUrl = "";
            ViewBag.Description = "";
            ViewBag.Url = "";
            ViewBag.Path = VirtualPathRootName;
            var b = Session["basket"] ?? new BasketM
            {
                BasketId = Guid.NewGuid()
            };
            return View(b);
        }

        public ActionResult RemoveFromBasket(int balloonid)
        {
            var b = (BasketM)Session["basket"];
            if (b != null)
            {
                if (b.Goods != null)
                {
                    b.Goods.Remove(b.Goods.SingleOrDefault(g => g.Balloon.BalloonId == balloonid));
                }
            }
            return RedirectToAction("Basket");
        }

        [HttpPost]
        public async Task<ActionResult> SaveBasket(BasketM basket)
        {
            if (string.IsNullOrEmpty(basket.Phone))
            {
                ViewBag.Message = "Не заполнен телефон";
                return View("Basket");
            }
            var b = (BasketM)Session["basket"];
            if (b != null)
            {
                var goods = BuildGoods(b);
                var description = basket.Description;
                if (!string.IsNullOrEmpty(basket.AddressTo))
                {
                    description += ", !!!Адрес доставки: (" + basket.AddressTo + ")!!!";
                }
                if (!string.IsNullOrEmpty(basket.DateTo))
                {
                    description += ", !!!Дата доставки: (" + basket.DateTo + ")!!!";
                }
                var newBasket = new Basket
                {
                    BasketNumber = b.BasketId.ToString(),
                    DateCreate = DateTime.Now,
                    Phone = basket.Phone,
                    FromName = basket.From,
                    Description = description,
                    Goods = goods
                };
                var modifiedBasket = basketRepository.Add(newBasket);
                await mailer.SendEmailAsync(modifiedBasket, goods);
                var result = new ResultModel
                {
                    Message = modifiedBasket
                };
                return View("SaveBasket", result);
            }

            return View();
        }

        private static string BuildGoods(BasketM b)
        {
            var goods = "<table class=\"table table-basket-order\">";
            goods += b.Goods
                .Select(
                    good =>
                    {
                        return string.Format(
                            "<tbody><tr><td>{0}({1})</td><td>{2}</td><td>{3}</td><td>{4}</td></tr></tbody>",
                            good.Balloon.Name, good.Balloon.Description, good.Count, good.Balloon.Price, GetPrice(good));
                    })
                .Aggregate("<thead><tr><th>Товар</th><th>Кол-во</th><th>Цена</th><th>Стоимость</th></tr></thead>",
                    (current, line) => current + line);

            goods += string.Format("<tr><td colspan=\"4\"><div class=\"pull-right\"><label>Общая стоимость: {0}</label>&nbsp;руб.</div></td><td></td></tr>", b.Goods.Sum(s => s.PriceWithDiscount * s.Count));
            goods += "</table>";
            return goods;
        }

        private static decimal? GetPrice(Good good)
        {
            var price = good.Count * good.PriceWithDiscount;
            return price;
        }

        [Authorize(Users = "admin")]
        public ActionResult GetBaskets()
        {
            var baskets = basketRepository
                .GetBaskets()
                .OrderByDescending(b => b.BasketId)
                .Take(25)
                .ToArray();

            return View(baskets);
        }

        [HttpPost]
        public async Task<ActionResult> NewRequest(Basket basket)
        {
            if (string.IsNullOrEmpty(basket.Phone))
            {
                return RedirectToAction("Index", "Home");
            }
            var b = (BasketM)Session["basket"];
            var basketNumber = b == null ? Guid.NewGuid().ToString() : b.BasketId.ToString();

            var goods = "";
            if (b != null)
            {
                if (b.Goods.Count > 0)
                {
                    goods = BuildGoods(b);
                }
            }

            var newBasket = new Basket
            {
                BasketNumber = basketNumber,
                DateCreate = DateTime.Now,
                Phone = basket.Phone,
                FromName = basket.FromName,
                Description = basket.Description,
                Goods = goods
            };
            var modifiedBasket = basketRepository.Add(newBasket);
            await mailer.SendEmailAsync(modifiedBasket, goods);
            var result = new ResultModel
            {
                Message = modifiedBasket
            };
            return View("SaveBasket", result);
        }

        [HttpGet]
        public ActionResult Search(string searchtext)
        {
            var cart = basketRepository.GetBaskets();
            if (!string.IsNullOrEmpty(searchtext))
            {
                cart = cart.Where(b => b.BasketId.ToString() == searchtext || b.FromName.Contains(searchtext));
            }
            return View("GetBaskets", cart.ToArray());
        }
    }
}