using System.Linq;
using SharikiApp.Core.Models;
using SharikiApp.Models.Cache;

namespace SharikiApp.Models
{
    public class BalloonProvider : IBalloonProvider
    {
        private readonly IDataCache dataCache;

        public BalloonProvider(IDataCache dataCache)
        {
            this.dataCache = dataCache;
        }

        public Balloon[] GetBalloons(int? balloonTypeId)
        {
            var collection = dataCache.GetBalloons().Values;
            return balloonTypeId.HasValue ? collection.Where(v => v.BalloonTypeId == balloonTypeId.Value).ToArray() : collection.ToArray();
        }

        public BalloonType[] GetBalloonsTypes()
        {
            return dataCache.GetBalloonTypes().Values.ToArray();
        }

        public News[] GetNews()
        {
            return dataCache.GetNews().Values.ToArray();
        }
    }
}