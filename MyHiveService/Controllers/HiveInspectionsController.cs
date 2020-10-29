using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyHiveService.Models;

namespace MyHiveService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HiveInspectionsController : CRUDControllerBase<HiveInspection>
    {
        public HiveInspectionsController(MyHiveDbContext context, ILogger<HiveInspectionsController> logger)
            : base(context, context.HiveInspections, logger)
        {
        }
    }
}
