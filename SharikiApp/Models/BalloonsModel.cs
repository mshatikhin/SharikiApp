using SharikiApp.Core.Models;

namespace SharikiApp.Models
{
    public class BalloonsModel
    {
        public BalloonType[] BalloonTypes { get; set; }
        public Balloon[] Balloons { get; set; }
        public BalloonType SelectedBalloonType { get; set; }
        public bool IsSearch { get; set; }
        public BalloonTypeM SelectedBalloonTypeM { get; set; }
    }
}