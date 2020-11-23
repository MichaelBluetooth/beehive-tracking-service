using MyHiveService.Models;
using MyHiveService.Models.DB;

namespace MyHive.Authentication
{
    public interface IUserService
    {
        bool IsValidUserCredentials(string username, string password);
        User findUserByUserName(string username);
        User Register(string username, string password);
        bool isValidPassword(string password);
    }
}