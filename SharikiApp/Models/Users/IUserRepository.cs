using System.Net;
using SharikiApp.Core.Models;

namespace SharikiApp.Models.Users
{
    public interface IUserRepository
    {
        User Save(NetworkCredential credentials);
        bool ExistUser(string userName);
        User GetUserByLogin(string login);
        bool UserIsAdmin(string login);
        User[] GetUsers();
    }
}