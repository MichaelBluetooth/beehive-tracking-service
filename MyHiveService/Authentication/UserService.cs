using System.Linq;
using System.Text.RegularExpressions;
using MyHiveService.Models.DB;

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
            return _ctx.Users.FirstOrDefault(u => u.username.ToLower() == username.ToLower());
        }

        public User Register(string username, string password)
        {
            User newUser = new User()
            {
                username = username,
                password = BCrypt.Net.BCrypt.HashPassword(password),
            };
            _ctx.Users.Add(newUser);

            return newUser;
        }

        public bool isValidPassword(string password)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            return hasUpperChar.IsMatch(password) &&
                   hasNumber.IsMatch(password) &&
                   hasMinimum8Chars.IsMatch(password);
        }
    }
}