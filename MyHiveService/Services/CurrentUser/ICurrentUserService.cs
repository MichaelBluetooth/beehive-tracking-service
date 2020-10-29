using MyHiveService.Models;

namespace MyHiveService.Services.CurrentUser
{
    public interface ICurrentUserService
    {
        User GetCurrentUser();
    }
}