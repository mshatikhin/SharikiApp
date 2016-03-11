using SharikiApp.Core.Models;

namespace SharikiApp.Models
{
    public interface IBalloonProvider
    {
        Balloon[] GetBalloons(int? balloonTypeId);
        BalloonType[] GetBalloonsTypes();
        News[] GetNews();
    }
}