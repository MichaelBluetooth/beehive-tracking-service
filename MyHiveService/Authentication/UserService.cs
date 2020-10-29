using System;
using System.Linq;
using MyHiveService.Models;

namespace MyHive.Authentication
{
    public class UserService : IUserService
    {
        private readonly MyHiveDbContext _ctx;

        public UserService(MyHiveDbContext ctx)
        {
            _ctx = ctx;
        }

        public bool IsValidUserCredentials(string username, string password)
        {
            bool isValid = false;

            if (!string.IsNullOrWhiteSpace(password) && !string.IsNullOrEmpty(username))
            {
                User user = findUserByUserName(username);
                if (null != user)
                {
                    isValid = BCrypt.Net.BCrypt.Verify(password, user.password);
                }
            }

            return isValid;
        }

        public User findUserByUserName(string username)
        {
            return _ctx.Users.FirstOrDefault(u => u.username == username);
        }
    }
}