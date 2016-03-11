using System;
using System.Linq;
using System.Net;
using SharikiApp.Core.Models;
using SharikiApp.Helpers;

namespace SharikiApp.Models.Users
{
    public class UserRepository : IUserRepository
    {
        public User Save(NetworkCredential credentials)
        {
            var usr = new User
            {
                Login = credentials.UserName,
                Password = AppHelper.HashAndSolt(credentials.Password),                
                IsAdmin = false
            };
            try
            {
                using (var db = new hellraz5_sharikiEntities())
                {
                    db.Users.Add(usr);
                    db.SaveChanges();
                    return usr;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool ExistUser(string userName)
        {
            using (var db = new hellraz5_sharikiEntities())
            {
                var usr = db.Users.FirstOrDefault(user => user.Login.ToLower() == userName);
                return usr != null;
            }
        }

        public User GetUserByLogin(string login)
        {
            using (var db = new hellraz5_sharikiEntities())
            {
                var usr = db.Users.FirstOrDefault(user => user.Login.ToLower() == login.ToLower());
                return usr;
            }
        }

        public bool UserIsAdmin(string login)
        {
            using (var db = new hellraz5_sharikiEntities())
            {
                var usr = db.Users.FirstOrDefault(user => user.Login.ToLower() == login.ToLower() && user.IsAdmin);
                return usr != null;
            }
        }

        public User[] GetUsers()
        {
            using (var db = new hellraz5_sharikiEntities())
            {                
                return db.Users.ToArray();
            }
        }
    }
}