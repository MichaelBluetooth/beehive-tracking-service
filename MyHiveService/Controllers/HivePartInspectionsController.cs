using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyHiveService.Models;

namespace MyHiveService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HivePartInspectionsController : CRUDControllerBase<HivePartInspection>
    {
        public HivePartInspectionsController(MyHiveDbContext context, ILogger<HivePartInspectionsController> logger)
            : base(context, context.HivePartInspections, logger)
        {
        }
    }
}
