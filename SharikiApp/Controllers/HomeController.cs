using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using Microsoft.Ajax.Utilities;
using SharikiApp.Core.Models;
using SharikiApp.Models;
using SharikiApp.Models.Mailer;

namespace SharikiApp.Controllers
{
    public class HomeController : Controller
    {
        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            base.OnAuthentication(filterContext);
        }
        private static readonly string VirtualPathRootName = ConfigurationManager.AppSettings["VirtualPathRootName"];
        private static readonly string SiteName = "http://sharikekb.ru/";
        private readonly IBalloonService balloonService;
        private readonly IMailer mailer;

        public HomeController(
            IBalloonService balloonService,
            IMailer mailer
            )
        {
            this.balloonService = balloonService;
            this.mailer = mailer;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Главная (шарики, воздушные шары, екатеринбург)";
            ViewBag.Type = "article";
            ViewBag.Description = "Воздушные шары в Екатеринбурге. Для вас! Праздничное оформление шарами, шары с доставкой, эксклюзивная печать на шарах, выгодное производство рекламного продукта!";
            ViewBag.Url = SiteName;
            ViewBag.Keywords = "шары с гелием, шарики, купить воздушные шары на визе, дядя женя шары, заказать шары екатеринбург";
            return RedirectToRoute("main");
        }

        public ActionResult Main()
        {
            ViewBag.Title = "Главная (шарики, воздушные шары, екатеринбург)";
            ViewBag.Type = "article";
            ViewBag.Description = "Воздушные шары в Екатеринбурге. Для вас! Праздничное оформление шарами, шары с доставкой, эксклюзивная печать на шарах, выгодное производство рекламного продукта!";
            ViewBag.Url = SiteName;
            ViewBag.Keywords = "шары с гелием, шарики, купить воздушные шары на визе, дядя женя шары, заказать шары екатеринбург";
            var news = balloonService.GetTopNews();
            ViewBag.Path = VirtualPathRootName;
            return View("Index", news);
        }

        public ActionResult About()
        {
            ViewBag.Title = "О нас (шарики екатеринбург) творческая мастерская Дядя Женя";
            ViewBag.Type = "article";
            ViewBag.Description = "Наши контакты: Фактический адрес местонахождения мастерской: г. Екатеринбург, ул. Долорес Ибарурри 2, 1 эт, оф 119. Почтовый адрес для отправки корреспонденции: 620028 г. Екатеринбург, ул. Виз Бульвар, 25, а/я 6. Контактные телефоны: тел. +7(343)328-15-67  сот. +7(922)11-22-866";
            ViewBag.Url = SiteName + "about";
            return View();
        }

        public ActionResult CommerceBalloon(TypeCommerce? type)
        {
            ViewBag.Title = "Заказать печать на воздушных шарах – Мастерская шариков Дядя Женя, Екатеринбург";
            ViewBag.Type = "article";
            ViewBag.Url = SiteName + "commerce";
            ViewBag.Tab = type;
            ViewBag.Keywords = "Печать на воздушных шарах, печать логотипа, рекламная, цена, заказать, Екатеринбург";
            ViewBag.Description = "Наши специалисты готовы выполнить печать на воздушных шарах, нанести веселые надписи и логотипы вашей компании. Мы гарантируем качество работы и выполняем заказы за минимальное время качественно и оперативно.";
            return View();
        }

        public ActionResult Pechat_na_sharah()
        {
            ViewBag.Title = "Заказать печать на воздушных шарах – Мастерская шариков Дядя Женя, Екатеринбург";
            ViewBag.Type = "article";
            ViewBag.Url = SiteName + "Pechat_na_sharah";
            ViewBag.Keywords = "Печать на воздушных шарах, заказ шаров с логотипом, воздушные шары с логотипом, с печатью логотипа, нанесение логотипа на шары, цена, купить, Екатеринбург";
            ViewBag.Description = "На нашей странице можно заказать печать на воздушных шариках разных видов. Мы внимательно прислушиваемся к пожеланиям клиентов и полностью гарантируем качество печати логотипов и надписей.";
            return View();
        }

        public ActionResult Panno_iz_sharov()
        {
            ViewBag.Title = "Панно из воздушных шаров | Мастерская шариков Дядя Женя, Екатеринбург";
            ViewBag.Type = "article";
            ViewBag.Url = SiteName + "Panno_iz_sharov";
            ViewBag.Keywords = "Заказать, Екатеринбург, панно из воздушных шаров";
            ViewBag.Description = "Панно — это отличная альтернатива для праздников и событий крупного масштаба. Такое оформление воздушными шарами пользуется особой популярностью в Екатеринбурге и благодаря своей прочности может использоваться как на закрытых, так и открытых площадках. Наша мастерская изготовит оригинальное панно для вас по доступной цене.";
            return View();
        }

        public ActionResult Izgotovlenie_flazhkov()
        {
            ViewBag.Title = "Изготовление флажков с логотипом | Мастерская шариков Дядя Женя, Екатеринбург";
            ViewBag.Type = "article";
            ViewBag.Url = SiteName + "Izgotovlenie_flazhkov";
            ViewBag.Keywords = "Екатеринбург, изготовление флажков, флажки с логотипом";
            ViewBag.Description = "Наша мастерская предлагает  оперативное  изготовление флажков по доступной цене в Екатеринбурге. Флажки являются отличной альтернативой стандартным листовкам, смогут привлечь внимание потенциальных клиентов к вашей организации, а также идеально подходят для промо мероприятий.";
            return View();
        }

        public ActionResult Specjeffekty()
        {
            ViewBag.Title = "Спецэффекты (шарики екатеринбург) творческая мастерская Дядя Женя";
            ViewBag.Type = "article";
            ViewBag.Url = SiteName + "Specjeffekty";
            ViewBag.Keywords = "печать на шарах, нанесение логотипа на шары, дядя женя, заказать флажки с логотипом, флажок бумажный, изготовление флажков дядя женя, панно из воздушных шаров, логотип из шаров, дядя женя,екатеринбург";

            return View();
        }

        public ActionResult Podarki()
        {
            ViewBag.Title = "Подарки (шарики екатеринбург) творческая мастерская Дядя Женя";
            ViewBag.Type = "article";
            ViewBag.Url = SiteName + "Podarki";
            ViewBag.Keywords = "печать на шарах, нанесение логотипа на шары, дядя женя, заказать флажки с логотипом, флажок бумажный, изготовление флажков дядя женя, панно из воздушных шаров, логотип из шаров, дядя женя,екатеринбург";

            return View();
        }

        public ActionResult Search(string searchText)
        {
            ViewBag.Title = "Шарики екатеринбург творческая мастерская Дядя Женя";
            ViewBag.Type = "article";
            ViewBag.Url = SiteName + "Podarki";
            ViewBag.Path = VirtualPathRootName;
            if (!string.IsNullOrEmpty(searchText))
            {
                var balloonsModel = balloonService.Build(null, null);
                balloonsModel.Balloons = SearchBalloons(searchText, balloonsModel);
                balloonsModel.IsSearch = true;
                return View("BalloonsResults", balloonsModel);
            }
            return View("BalloonsResults");
        }

        public ActionResult Balloons(BalloonTypeM? type)
        {
            ViewBag.Type = "article";
            ViewBag.Url = SiteName + "selectballoons";
            ViewBag.Path = VirtualPathRootName;
            ViewBag.Title = "Заказать воздушные шары – Мастерская шариков Дядя Женя, Екатеринбург";
            ViewBag.Keywords = "Воздушные шары, с гелием, шарики, детские, наполненные гелием, доставка, шары на заказ, гелиевые шары с доставкой, цена, купить, Екатеринбург";
            ViewBag.Description =
                "Предлагаем воздушные шары на любой вкус оптом и в розницу. Наши клиенты могут заказать не только шары, но также композиции и фигуры из гелиевых шариков и прочую продукцию на выгодных условиях.";
            BalloonTypeM? balloonType = null;
            if (type.HasValue)
            {
                balloonType = type.Value;
            }
            var exceptBalloonTypes = new[]
            {
                (int)BalloonTypeM.Arenda,
                (int)BalloonTypeM.ElementsOfDecor,
                (int)BalloonTypeM.DecoreBirthDay,
                (int)BalloonTypeM.RodDom,
                (int)BalloonTypeM.Svadba,
                (int)BalloonTypeM.Avto,
                (int)BalloonTypeM.SvetDecor,
                (int)BalloonTypeM.NewYearDecor,
                (int)BalloonTypeM.DetskiPrazdnik,
                (int)BalloonTypeM.Halloween,
                (int)BalloonTypeM.Mujchinam,
                (int)BalloonTypeM.DayPobedi,
                (int)BalloonTypeM.P8marta,
                (int)BalloonTypeM.Vipusknoy,
                (int)BalloonTypeM.VipusknoyVsadike,
                (int)BalloonTypeM.TorjestOpen,
                (int)BalloonTypeM.Lubimim,
            };
            var balloonsModel = balloonService.Build((int?)balloonType, exceptBalloonTypes);

            switch (balloonType)
            {
                case BalloonTypeM.Latex:
                    ViewBag.Title = "Заказать латексные воздушные шары – Мастерская шариков Дядя Женя, Екатеринбург";
                    ViewBag.Keywords = "Латексные воздушные шары, оптом, с рисунком, цена, купить, Екатеринбург";
                    ViewBag.Description = "Латексные шары отличного качества предлагаем заказать в нашей мастерской. Мы гарантируем оптимальные цены, большой ассортимент продукции и доставляем заказы по любым адресам в Екатеринбурге.";
                    break;
                case BalloonTypeM.Folgirov:
                    ViewBag.Title = "Заказать фольгированные воздушные шары в виде героев мультфильмов | Мастерская шариков Дядя Женя, Екатеринбург";
                    ViewBag.Keywords = "Цена, купить, Екатеринбург, воздушные шары в виде героев мультфильмов";
                    ViewBag.Description = "Фольгированные воздушные шары — это идеальное украшение из фольги для детских дней рождений и других подобных событий. Наша мастерская предлагает качественные шары для девочек и мальчиков разных возрастов по приемлемым ценам. У нас всегда имеются креативные идеи для праздника.";
                    break;
                case BalloonTypeM.Svet:
                    ViewBag.Title = "Заказать светящиеся шары | Мастерская шариков Дядя Женя, Екатеринбург";
                    ViewBag.Keywords = "Цена, купить, Екатеринбург, светящиеся шары";
                    ViewBag.Description = "Светящиеся шары на пике популярности. Это отличное дополнение к основному подарку и идеальное решение для романтических вечеров и свадеб. Наша мастерская предлагает большой выбор шаров в Екатеринбурге, доступные расценки и высокий сервис для каждого клиента.";
                    break;
                case BalloonTypeM.Buket:
                    ViewBag.Title = "Заказать букеты из шаров, цена | Мастерская шариков Дядя Женя, Екатеринбург";
                    ViewBag.Keywords = "Цена, купить, Екатеринбург, букеты из шаров";
                    ViewBag.Description = "Букеты из шаров — это достойная альтернатива привычным цветочным букетам. Наша мастерская предлагает вам множество оригинальных композиций для детских и взрослых праздников по самым доступным ценам в Екатеринбурге. У нас вы сможете найти действительно креативное решение для своего торжества.";
                    break;
                case BalloonTypeM.Figurki:
                    ViewBag.Title = "Заказать цветы и фигурки из шаров – Мастерская шариков Дядя Женя, Екатеринбург";
                    ViewBag.Keywords = "Цветы и фигурки из шаров, цена, заказать, Екатеринбург";
                    ViewBag.Description = "Яркие и красочные цветы и фигурки из шаров можно выгодно приобрести на странице нашей компании. Мы предлагаем сделать заказ в режиме онлайн или по телефону.";
                    break;
                case BalloonTypeM.GoodPrazdnik:
                    ViewBag.Title = "Заказать товары для детского праздника – Мастерская шариков Дядя Женя, Екатеринбург";
                    ViewBag.Keywords = "Товары для детского праздника, для оформления, для украшения, для дня рождения, оптом, цена, заказать, Екатеринбург";
                    ViewBag.Description = "Широкий выбор товаров для детского праздника вы найдете в нашем каталоге. Купить их можно быстро и по очень доступной цене.";
                    break;
            }
            return View(balloonsModel);
        }

        private static Balloon[] SearchBalloons(string searchText, BalloonsModel model)
        {
            searchText = searchText.ToLower();
            return model.Balloons
                .Where(b => !b.Hide)
                .Where(b =>
                    (b.Name != null && b.Name.ToLower().Contains(searchText)) ||
                    (b.Description != null && b.Description.ToLower().Contains(searchText))
                )
                .OrderBy(b => b.Name)
                .DistinctBy(b => b.Name.Trim())
                .ToArray();

        }

        public ActionResult Decorate(BalloonTypeM? type, string searchText)
        {
            ViewBag.Title = "Заказать оформление шарами, цена | Мастерская шариков Дядя Женя, Екатеринбург";
            ViewBag.Type = "article";
            ViewBag.Url = SiteName + "decorate";
            ViewBag.Tab = type;
            ViewBag.Path = VirtualPathRootName;
            ViewBag.Keywords = "Оформление шарами, украшение шарами, украшение воздушными шарами, Екатеринбург";
            ViewBag.Description = "Оформление шарами — это красиво и оригинально. К тому же это отличный способ придать событию торжественности, а также создать особую атмосферу праздника. Наша мастерская предлагает множество идей для украшения шарами, среди которых вы сможете найти оптимальный вариант для своего случая.";

            BalloonTypeM? balloonType = null;
            if (type.HasValue)
            {
                balloonType = type.Value;
            }

            var exceptBalloonTypes = new[]
            {
                (int)BalloonTypeM.Arenda,
                (int)BalloonTypeM.None,
                (int)BalloonTypeM.Latex,
                (int)BalloonTypeM.Folgirov,
                (int)BalloonTypeM.Buket,
                (int)BalloonTypeM.Figurki,
                (int)BalloonTypeM.Svet,
                (int)BalloonTypeM.Speceffets,
                (int)BalloonTypeM.PodarkiSjurprizy,
                (int)BalloonTypeM.GoodPrazdnik,
                (int)BalloonTypeM.Arenda,
                (int)BalloonTypeM.Palitra,
                (int)BalloonTypeM.SvetDecor,
                (int)BalloonTypeM.Avto,
                (int)BalloonTypeM.NewYearDecor,
                (int)BalloonTypeM.Accsesuars
            };

            var balloonsModel = balloonService.Build((int?)balloonType, exceptBalloonTypes);
            if (!string.IsNullOrEmpty(searchText))
            {
                balloonsModel.Balloons = SearchBalloons(searchText, balloonsModel);
                balloonsModel.IsSearch = true;

                if (balloonsModel.Balloons.Any())
                    return View(balloonsModel);

                return RedirectToAction("Index");
            }
            switch (balloonType)
            {
                case BalloonTypeM.DecoreBirthDay:
                    ViewBag.Title = "Заказать оформление воздушными шарами на день рождения – Мастерская шариков Дядя Женя, Екатеринбург";
                    ViewBag.Keywords = "Оформление воздушными шарами на день рождения, оформление детского дня рождения, шары с гелием на день рождения, украшение, цена, купить, Екатеринбург";
                    ViewBag.Description = "Оформление воздушными шарами на день рождения позволит превратить стандартный праздник в сказочное торжество. Вы сможете выбрать красочные шарики разных видов, заказать букеты, гирлянды или композиции из шаров.";
                    break;
                case BalloonTypeM.ElementsOfDecor:
                    ViewBag.Title = "Заказать оформление шарами для любого праздника – Мастерская шариков Дядя Женя, Екатеринбург";
                    ViewBag.Keywords = "Для любого праздника, цена, заказать, Екатеринбург";
                    ViewBag.Description = "Заказывайте шары для любого праздника по самой лучшей цене в Екатеринбурге на странице нашей компании! Мы гарантируем качество продукции и помогаем выбирать шары на каждый праздник.";
                    break;
                case BalloonTypeM.Svadba:
                    ViewBag.Title = "Заказать оформление шарами свадеб | Мастерская шариков Дядя Женя, Екатеринбург";
                    ViewBag.Keywords = "Оформление воздушными шарами свадьбы, оформление зала на свадьбу, цена, заказать, Екатеринбург";
                    ViewBag.Description = "Хотите заказать оформление воздушными шарами свадьбы? Воспользуйтесь услугами нашей компании! Мы осуществим оформление зала в выбранном вами стиле качественно и недорого.";
                    break;
                case BalloonTypeM.RodDom:
                    ViewBag.Title = "Заказать шары на выписку из роддома | Мастерская шариков Дядя Женя, Екатеринбург";
                    ViewBag.Keywords = "Шары на выписку из роддома, украшение на выписку из роддома, Екатеринбург";
                    ViewBag.Description = "Рождение ребенка — это знаменательное событие в жизни молодой семьи. Наша мастерская предлагает вам сегодня креативное украшение на выписку из роддома, которое позволит создать нужную атмосферу и подчеркнуть всю торжественность этого дня, подарить незабываемые ощущения и светлые чувства.";
                    break;
                case BalloonTypeM.DetskiPrazdnik:
                    ViewBag.Title = "Заказать оформление детского праздника воздушными шарами – Мастерская шариков Дядя Женя, Екатеринбург";
                    ViewBag.Keywords = "Оформление детского праздника воздушными шарами, украшение, фигуры из шаров на детский праздник, цена, заказать, Екатеринбург";
                    ViewBag.Description = "Мы готовы оперативно и недорого оформить детский праздник воздушными шарами в любом кафе или квартире в Екатеринбурге. Подарите ребенку незабываемый праздник с нашей помощью!";
                    break;
                case BalloonTypeM.Halloween:
                    break;
                case BalloonTypeM.Mujchinam:
                    ViewBag.Title = "Заказать оформление шарами мужчинам – Мастерская шариков Дядя Женя, Екатеринбург";
                    ViewBag.Keywords = "Мужчинам, цена, заказать, Екатеринбург";
                    ViewBag.Description = "Предлагаем заказать шары в качестве подарка мужчинам на нашем сайте по самой выгодной цене в Екатеринбурге. Такой необычный презент вызовет незабываемые эмоции у вашего близкого человека.";
                    break;
                case BalloonTypeM.DayPobedi:
                    break;
                case BalloonTypeM.P8marta:
                    break;
                case BalloonTypeM.Vipusknoy:
                    break;
                case BalloonTypeM.VipusknoyVsadike:
                    break;
                case BalloonTypeM.TorjestOpen:
                    break;
                case BalloonTypeM.NewYear:
                    break;
                case BalloonTypeM.Lubimim:
                    ViewBag.Title = "Заказать оформление шарами любимым – Мастерская шариков Дядя Женя, Екатеринбург";
                    ViewBag.Keywords = "Любимым, цена, заказать, Екатеринбург";
                    ViewBag.Description = "Дарите любимым не только цветы, но и романтичные шарики в форме сердца. У нас на сайте можно заказать оригинальные шары со смайлами, цветы, а также другую продукцию.";
                    break;
                case BalloonTypeM.VipuskSchool:
                    ViewBag.Title = "Заказать оформление шарами выпускного в школе  – Мастерская шариков Дядя Женя, Екатеринбург";
                    ViewBag.Keywords = "Оформление шарами выпускного в школе, выпускных вечеров, на последний звонок, на выпускной, украшение зала шарами, воздушные шары, цена, купить, Екатеринбург";
                    ViewBag.Description = "Специалисты нашей мастерской возьмут на себя оформление шарами выпускного, предложат множество вариантов и помогут выбрать самые подходящие из них. Мы сделаем ваш праздник незабываемым!";
                    break;
                case BalloonTypeM.OneMaya:
                    ViewBag.Title = "Заказать доставку воздушных шаров на 1 мая – Мастерская шариков Дядя Женя, Екатеринбург";
                    ViewBag.Keywords = "Доставка воздушных шаров на 1 мая, воздушные шары на 1 мая, цена, купить, Екатеринбург";
                    ViewBag.Description = "Выбрав доставку воздушных шаров на 1 мая от нашей мастерской, вы сможете сэкономить собственные средства и время, а также насладиться праздничной атмосферой. Предлагаем широкий выбор шариков разных цветов.";
                    break;
                case BalloonTypeM.NineMaya:
                    ViewBag.Title = "Заказать воздушные шары на 9мая – Мастерская шариков Дядя Женя, Екатеринбург";
                    ViewBag.Keywords = "Воздушные шары на 9 мая, украшение шарами на 9 мая, оформление шарами, цена, купить, Екатеринбург";
                    ViewBag.Description = "Заказать воздушные шары на 9 мая по достаточно привлекательной цене можно на нашем сайте. Мы предлагаем отличный выбор цветовых и стилистических решений и помогаем оформить любые помещения воздушными шарами.";
                    break;
                default:
                    break;
            }
            return View(balloonsModel);
        }

        [HttpGet]
        public ActionResult Warm()
        {
            balloonService.Warm();
            return Content("Ok");
        }

        public ActionResult RequestInfo()
        {
            return View("Index");
        }

        public ActionResult Effects()
        {
            ViewBag.Title = "Спецэффекты (шарики екатеринбург) творческая мастерская Дядя Женя";
            ViewBag.Type = "article";
            return View();
        }

        public ActionResult Gifts()
        {
            ViewBag.Title = "Подарки (шарики екатеринбург) творческая мастерская Дядя Женя";
            ViewBag.Type = "article";
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CallMe(string fromName, string phone)
        {
            ViewBag.Message = "Вам перезвонят";
            await mailer.SendEmailAsync(null, string.Format("Ожидаю звонка: {0}.<br/> Телефон: {1}", fromName, phone));
            return RedirectToAction("Index");
        }

        public ActionResult Svet()
        {
            ViewBag.Title = "Декор и свет (шарики екатеринбург) творческая мастерская Дядя Женя";
            ViewBag.Type = "article";
            ViewBag.Url = SiteName + "svet";
            ViewBag.Keywords = "большой бант на авто заказать";
            ViewBag.Path = VirtualPathRootName;
            var model = balloonService.Build(null, null).Balloons.Where(b => b.BalloonTypeId == (int)BalloonTypeM.SvetDecor).ToArray();
            return View("Svet", model);
        }

        public ActionResult Auto()
        {
            ViewBag.Title = "Заказать подарочный бант на автомобиль – Мастерская шариков Дядя Женя, Екатеринбург";
            ViewBag.Type = "article";
            ViewBag.Url = SiteName + "svet";
            ViewBag.Keywords = "Подарочный бант на автомобиль, большой, цена, заказать, Екатеринбург";
            ViewBag.Description =
                "Особый подарок требует красочного оформления. Заказывайте подарочный бант на автомобиль на нашей странице и превращайте его вручение в особое торжество!";
            ViewBag.Path = VirtualPathRootName;
            var model = balloonService.Build(null, null).Balloons.Where(b => b.BalloonTypeId == (int)BalloonTypeM.Avto).ToArray();
            return View(model);
        }

        public ActionResult NewYear()
        {
            ViewBag.Title = "Новогоднее оформление фасада здания шарами";
            ViewBag.Type = "article";
            ViewBag.Url = SiteName + "svet";
            ViewBag.Keywords = "Новогоднее оформление фасада здания, фасадов домов, световое оформление";
            ViewBag.Description = "Наша компания осуществляет новогоднее оформление фасада здания по приемлемым ценам. Мы гарантируем создание фееричной иллюминации, которая создаст атмосферу настоящего праздника. Стоимость наших услуг является приемлемой.";
            ViewBag.Path = VirtualPathRootName;
            var model = balloonService.Build(null, null).Balloons.Where(b => b.BalloonTypeId == (int)BalloonTypeM.NewYearDecor).ToArray();
            return View(model);
        }

        public ActionResult Arenda()
        {
            ViewBag.Title = "Аренда оборудования (шарики екатеринбург) творческая мастерская Дядя Женя";
            ViewBag.Type = "article";
            ViewBag.Url = SiteName + "arenda";
            ViewBag.Path = VirtualPathRootName;
            ViewBag.Keywords = "стойка для баннера в аренду, красная ковровая дорожка в аренду";
            ViewBag.Path = VirtualPathRootName;
            var model = balloonService.Build((int?)BalloonTypeM.Arenda, null);
            return View(model);
        }

        public ActionResult Dekor_i_svet()
        {
            ViewBag.Title = "Новогоднее оформление в Екатеринбурге";
            ViewBag.Type = "article";
            ViewBag.Url = SiteName + "svet";
            ViewBag.Keywords = "Новогоднее оформление, новогоднее оформление холла в офисе, уличное";
            ViewBag.Description = "Наша компания осуществляет новогоднее оформление офисов и других помещений. Мы позаботимся о том, чтобы ваше здание встретило праздники по-настоящему нарядным, создавая одним своим видом непередаваемое новогоднее настроение.";
            ViewBag.Path = VirtualPathRootName;
            return View("Svet");
        }

        public ActionResult BalloonsTop()
        {
            var exceptBalloonTypes = new[] {
                (int)BalloonTypeM.NewYear,
                (int)BalloonTypeM.NewYearDecor
                };
            var model = balloonService.Build(null, exceptBalloonTypes).Balloons.OrderBy(r => Guid.NewGuid()).Take(24).ToArray();
            ViewBag.Path = VirtualPathRootName;
            return View("BalloonsTop", model);
        }

        public string GetArticle(ArticleTypeM type)
        {
            var article = balloonService.GetArticle(type);
            return article == null ? "" : article.ContentText;
        }
    }
}