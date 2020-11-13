using MyHiveService.Models;

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