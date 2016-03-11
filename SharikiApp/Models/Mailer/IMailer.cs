using System.Threading.Tasks;
using SharikiApp.Core.Models;

namespace SharikiApp.Models.Mailer
{
    public interface IMailer
    {
        void SendEmail(Basket modifiedBasket, string textBody);
        Task SendEmailAsync(Basket modifiedBasket, string textBody);
    }
}