using System.Linq;
using SharikiApp.Core.Models;

namespace SharikiApp.Models
{
    public interface IBalloonRepository
    {
        Balloon Add(Balloon balloon);
        void Remove(int id);
        IQueryable<Balloon> GetBalloons(int? balloonType);
        IQueryable<BalloonType> GetBalloonsTypes();
        void SaveBalloon(Balloon balloon);
        void SaveBalloonImage(Balloon[] balloonId);
        Balloon GetBalloonById(int balloonId);
    }
}