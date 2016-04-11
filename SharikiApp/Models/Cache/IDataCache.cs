using System.Collections.Generic;
using SharikiApp.Core.Models;

namespace SharikiApp.Models.Cache
{
    public interface IDataCache
    {
        void ReloadBalloons();
        void ReloadTypes();
        void ReloadNews();
        void ReloadAll();
        IDictionary<int, Balloon> GetBalloons();
        IDictionary<int, BalloonType> GetBalloonTypes();
        IDictionary<int, News> GetNews();
        IDictionary<int, Article> GetArticles();
    }
}