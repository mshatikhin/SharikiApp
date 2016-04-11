using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using SharikiApp.Controllers;
using SharikiApp.Core.Models;

namespace SharikiApp.Models
{
    public class BalloonService : IBalloonService
    {
        private readonly IBalloonProvider balloonProvider;

        public BalloonService(IBalloonProvider balloonProvider)
        {
            this.balloonProvider = balloonProvider;
        }

        public BalloonsModel Build(int? balloonTypeId, int[] exceptBalloonTypes)
        {
            var balloons = balloonProvider.GetBalloons(balloonTypeId)
                .Where(b => !b.Hide)
                .ToArray();
            var types = balloonProvider
                .GetBalloonsTypes()
                .Where(t => t.Balloons.Any())
                .ToArray();
            if (exceptBalloonTypes != null)
            {
                balloons = balloons.Where(b => !exceptBalloonTypes.Contains(b.BalloonTypeId)).ToArray();
                types = types.Where(t => !exceptBalloonTypes.Contains(t.BalloonTypeId)).ToArray();
            }
            var selectedBalloonType = types.FirstOrDefault(t => t.BalloonTypeId == balloonTypeId);
            return new BalloonsModel
            {
                Balloons = balloons,
                BalloonTypes = types,
                SelectedBalloonType = selectedBalloonType,
                SelectedBalloonTypeM = selectedBalloonType != null ? (BalloonTypeM)selectedBalloonType.BalloonTypeId : BalloonTypeM.None,
                IsSearch = false
            };
        }

        public void Warm()
        {
            balloonProvider.GetBalloons(null);
            balloonProvider.GetBalloonsTypes();
            balloonProvider.GetNews();
            balloonProvider.GetArticles();
        }

        public News GetTopNews()
        {
            return balloonProvider.GetNews().LastOrDefault();
        }

        public Balloon SaveImage(Balloon balloon, string mapPath, HttpPostedFileBase file)
        {
            var filename = string.Format("{0}.jpg", balloon.BalloonId);
            var path = Path.Combine(mapPath, filename);
            if (file != null)
            {
                using (var image = Image.FromStream(file.InputStream))
                {
                    try
                    {
                        var d = new DirectoryInfo(mapPath);
                        if (d.Exists)
                        {
                            image.Save(path, ImageFormat.Jpeg);
                            image.Dispose();
                            balloon.BalloonImage = filename;
                            return balloon;
                        }
                    }
                    catch (Exception e)
                    {
                        balloon.BalloonImage = "null";
                        return null;
                    }
                }
            }
            return null;
        }

        public Article GetArticle(ArticleTypeM articleTypeM)
        {
            return balloonProvider.GetArticles().SingleOrDefault(a => a.ArticleId == (int)articleTypeM);
        }
    }
}