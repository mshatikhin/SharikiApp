using SharikiApp.Core.Models;

namespace SharikiApp.Models
{
    public class BalloonModel
    {
        public Balloon Balloon { get; set; }
        public BalloonType[] BalloonTypes { get; set; }
        public string Header { get; set; }
        public string Text { get; set; }
    }
}