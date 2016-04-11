using System.Web;
using SharikiApp.Controllers;
using SharikiApp.Core.Models;

namespace SharikiApp.Models
{
    public interface IBalloonService
    {
        void Warm();
        BalloonsModel Build(int? balloonTypeId, int[] exceptBalloonTypes);
        News GetTopNews();
        Balloon SaveImage(Balloon balloon, string mapPath, HttpPostedFileBase image);
        Article GetArticle(ArticleTypeM articleTypeM);
    }
}