using System;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using SharikiApp.Core.Models;
using SharikiApp.Helpers;

namespace SharikiApp.Models.Mailer
{
    public class Mailer: IMailer
    {
        public void SendEmail(Basket modifiedBasket, string textBody)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Host = "smtp.mail.ru";
                smtpClient.Port = 2525;
                const string address = "priemka@list.ru";
                const string password = "ee21021983ee";
                const string displayName = "sharikekb.ru";

                smtpClient.Credentials = new NetworkCredential(address, password);
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Timeout = 20000;
                var body = "";
                if (modifiedBasket != null)
                {
                    body = string.Format("Заказ #: {0} от {1}. <br/> Комментарии: {2}. <br/> Телефон: {3}.<br/>{4}",
                        modifiedBasket.BasketId, modifiedBasket.FromName, modifiedBasket.Description,
                        modifiedBasket.Phone, textBody);
                }
                else
                {
                    body = textBody;
                }
                var mail = new MailMessage
                {
                    From = new MailAddress(address, displayName),
                    Body = body,
                    IsBodyHtml = true,
                    Subject = "Заявка с сайта " + displayName,
                    Priority = MailPriority.High
                };

                mail.To.Add(new MailAddress(address));

                smtpClient.Send(mail);
            }
        }

        public async Task SendEmailAsync(Basket modifiedBasket, string textBody)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Host = "smtp.mail.ru";
                smtpClient.Port = 2525;
                const string address = "priemka@list.ru";
                const string password = "ee21021983ee";
                const string displayName = "sharikekb.ru";

                smtpClient.Credentials = new NetworkCredential(address, password);
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Timeout = 20000;
                var body = "";
                if (modifiedBasket != null)
                {
                    body = string.Format("Заказ #: {0} от {1}. <br/> Комментарии: {2}. <br/> Телефон: {3}.<br/>{4}",
                        modifiedBasket.BasketId, modifiedBasket.FromName, modifiedBasket.Description,
                        modifiedBasket.Phone, textBody);
                }
                else
                {
                    body = textBody;
                }
                var mail = new MailMessage
                {
                    From = new MailAddress(address, displayName),
                    Body = body,
                    IsBodyHtml = true,
                    Subject = "Заявка с сайта " + displayName,
                    Priority = MailPriority.High
                };

                mail.To.Add(new MailAddress(address));

                //smtpClient.SendMailAsync(mail);
                await smtpClient.SendMailAsync(mail);
            }
        }
    }
}