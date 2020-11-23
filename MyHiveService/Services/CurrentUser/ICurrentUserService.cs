using MyHiveService.Models.DB;

namespace MyHiveService.Services.CurrentUser
{
    public interface ICurrentUserService
    {
        User GetCurrentUser();
    }
}