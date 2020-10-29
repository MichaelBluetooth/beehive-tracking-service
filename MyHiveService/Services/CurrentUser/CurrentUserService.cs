using System.Linq;
using Microsoft.AspNetCore.Http;
using MyHiveService.Models;

namespace MyHiveService.Services.CurrentUser
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly MyHiveDbContext _ctx;
        private readonly IHttpContextAccessor _accessor;

        public CurrentUserService(MyHiveDbContext ctx, IHttpContextAccessor accessor)
        {
            _ctx = ctx;
            _accessor = accessor;
        }

        public User GetCurrentUser()
        {
            return _ctx.Users.FirstOrDefault(u => u.username == _accessor.HttpContext.User.Identity.Name);
        }
    }
}