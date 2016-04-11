using System.Collections.Generic;
using System.Linq;
using SharikiApp.Core.Models;

namespace SharikiApp.Models.Cache
{
    public class DataCache : IDataCache
    {
        private object syncRoot = new object();
        private IDictionary<int, Balloon> balloons;
        private IDictionary<int, BalloonType> balloonTypes;
        private IDictionary<int, News> news;
        private IDictionary<int, Article> articles;

        private readonly IBalloonRepository balloonRepository;
        private readonly INewsRepository newsRepository;
        private readonly IArticleRepository articleRepository;

        public DataCache(
            IBalloonRepository balloonRepository, 
            INewsRepository newsRepository, 
            IArticleRepository articleRepository)
        {
            this.balloonRepository = balloonRepository;
            this.newsRepository = newsRepository;
            this.articleRepository = articleRepository;
        }

        public void ReloadBalloons()
        {
            lock (syncRoot)
            {
                balloons = BuildDictionaryBalloons();
            }
        }

        public void ReloadTypes()
        {
            lock (syncRoot)
            {
                balloonTypes = BuildDictionaryBalloonTypes();
            }
        }

        public void ReloadNews()
        {
            lock (syncRoot)
            {
                news = BuildDictionaryNews();
            }
        }

        public void ReloadAll()
        {
            lock (syncRoot)
            {
                balloons = BuildDictionaryBalloons();
                news = BuildDictionaryNews();
                balloonTypes = BuildDictionaryBalloonTypes();
            }
        }

        private IDictionary<int, Balloon> BuildDictionaryBalloons()
        {
            var balloonsArray = balloonRepository
                .GetBalloons(null)
                .ToArray();
            return balloonsArray.ToDictionary(p => p.BalloonId);
        }

        public IDictionary<int, Balloon> GetBalloons()
        {
            lock (syncRoot)
            {
                return balloons ?? (balloons = BuildDictionaryBalloons());
            }
        }

        public IDictionary<int, BalloonType> GetBalloonTypes()
        {
            lock (syncRoot)
            {
                return balloonTypes ?? (balloonTypes = BuildDictionaryBalloonTypes());
            }
        }

        public IDictionary<int, News> GetNews()
        {
            lock (syncRoot)
            {
                return news ?? (news = BuildDictionaryNews());
            }
        }

        public IDictionary<int, Article> GetArticles()
        {
            lock (syncRoot)
            {
                return articles ?? (articles = BuildDictionaryArticles());
            }
        }

        private IDictionary<int, News> BuildDictionaryNews()
        {
            var newses = newsRepository.GetNews().ToArray();
            return newses.ToDictionary(p => p.NewsId);
        }

        private IDictionary<int, BalloonType> BuildDictionaryBalloonTypes()
        {
            var balloonTypesArray = balloonRepository.GetBalloonsTypes().ToArray();
            return balloonTypesArray.ToDictionary(p => p.BalloonTypeId);
        }

        private IDictionary<int, Article> BuildDictionaryArticles()
        {
            var arr = articleRepository.GetArticles().ToArray();
            return arr.ToDictionary(p => p.ArticleId);
        }
    }
}