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

        private readonly IBalloonRepository balloonRepository;
        private readonly INewsRepository newsRepository;

        public DataCache(IBalloonRepository balloonRepository, INewsRepository newsRepository)
        {
            this.balloonRepository = balloonRepository;
            this.newsRepository = newsRepository;
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
    }
}