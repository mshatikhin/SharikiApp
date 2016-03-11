using System.Web.Mvc;
using System.Web.Routing;

namespace SharikiApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
             name: "Search",
             url: "search/{searchText}",
             defaults: new { action = "Search", controller = "Home" }
             );

            routes.MapRoute(
             name: "Balloon",
             url: "balloon/{id}",
             defaults: new { action = "balloon", controller = "balloons", id = UrlParameter.Optional }
             );

            routes.MapRoute(
              name: "dekor_i_svet",
              url: "dekor_i_svet",
              defaults: new { action = "Svet", controller = "Home", id = UrlParameter.Optional }
              );

            routes.MapRoute(
              name: "auto",
              url: "dekor_i_svet/auto",
              defaults: new { action = "auto", controller = "Home", id = UrlParameter.Optional }
              );

            routes.MapRoute(
             name: "novii_god_decor",
             url: "dekor_i_svet/novii_god_decor",
             defaults: new { action = "newYear", controller = "Home", id = UrlParameter.Optional }
             );

            routes.MapRoute(
              name: "Svet",
              url: "svet",
              defaults: new { action = "Svet", controller = "Home", id = UrlParameter.Optional }
              );

            routes.MapRoute(
               name: "Arenda_oborudovanija",
               url: "arenda_oborudovanija",
               defaults: new { action = "Arenda", controller = "Home", id = UrlParameter.Optional }
               );

            routes.MapRoute(
               name: "Arenda",
               url: "arenda",
               defaults: new { action = "Arenda", controller = "Home", id = UrlParameter.Optional }
               );

            routes.MapRoute(
                name: "panno_iz_sharov",
                url: "reklamnaja_produkcija/panno_iz_sharov",
                defaults: new { action = "panno_iz_sharov", controller = "Home", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "izgotovlenie_flazhkov",
                url: "reklamnaja_produkcija/izgotovlenie_flazhkov",
                defaults: new { action = "izgotovlenie_flazhkov", controller = "Home", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "pechat_na_sharah",
                url: "reklamnaja_produkcija/pechat_na_sharah",
                defaults: new { action = "Pechat_na_sharah", controller = "Home", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "reklamnaja_produkcija",
                url: "reklamnaja_produkcija",
                defaults: new { action = "CommerceBalloon", controller = "Home", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Commerce",
                url: "commerce",
                defaults: new { action = "CommerceBalloon", controller = "Home", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Basket",
                url: "cart",
                defaults: new { action = "Basket", controller = "Basket", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "O nas",
                url: "o_nas",
                defaults: new { action = "About", controller = "Home", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "About",
                url: "about",
                defaults: new { action = "About", controller = "Home", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Gifts",
                url: "gifts",
                defaults: new { action = "Gifts", controller = "Home", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Effects",
                url: "effects",
                defaults: new { action = "Effects", controller = "Home", id = UrlParameter.Optional }
                );
            
            routes.MapRoute(
               name: "specjeffekty",
               url: "shary_i_podarki/specjeffekty",
               defaults: new { action = "specjeffekty", controller = "Home", id = UrlParameter.Optional }
               );

            routes.MapRoute(
               name: "podarki",
               url: "shary_i_podarki/podarki",
               defaults: new { action = "podarki", controller = "Home", id = UrlParameter.Optional }
               );

            routes.MapRoute(
               name: "oformlenie_sharami",
               url: "oformlenie_sharami/{type}",
               defaults: new { action = "Decorate", controller = "Home", type = UrlParameter.Optional }
               );
            
            routes.MapRoute(
                name: "Decorate",
                url: "decorate",
                defaults: new { action = "Decorate", controller = "Home", id = UrlParameter.Optional }
                );

            routes.MapRoute(
               name: "shary_i_podarki",
               url: "shary_i_podarki/{type}",
               defaults: new { action = "Balloons", controller = "Home", type = UrlParameter.Optional }
               );

            routes.MapRoute(
               name: "shariki",
               url: "shary_i_podarki/shariki/{type}",
               defaults: new { action = "Balloons", controller = "Home", type = UrlParameter.Optional }
               );

            routes.MapRoute(
               name: "Selectballoons",
               url: "selectballoons",
               defaults: new { action = "Balloons", controller = "Home", id = UrlParameter.Optional }
               );

            routes.MapRoute(
                name: "login",
                url: "login",
                defaults: new { action = "Login", controller = "Account", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "logOff",
                url: "logOff",
                defaults: new { action = "LogOff", controller = "Account", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "register",
                url: "register",
                defaults: new { action = "Register", controller = "Account", id = UrlParameter.Optional }
                );

            routes.MapRoute(
               name: "Main",
               url: "main",
               defaults: new { controller = "Home", action = "Main", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
