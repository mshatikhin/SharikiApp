using System.Linq;
using SharikiApp.Core.Models;

namespace SharikiApp.Helpers
{
    public class AuthorizationService
    {
        public virtual bool ValidateUser(string username, string password)
        {
            using (var db = new hellraz5_sharikiEntities())
            {
                password = AppHelper.HashAndSolt(password);
                var user = db.Users.FirstOrDefault(x => x.Login == username && x.Password == password);
                if (user != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}