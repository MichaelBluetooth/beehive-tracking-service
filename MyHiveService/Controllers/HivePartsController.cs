using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyHiveService.Models;

namespace MyHiveService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HivePartsController : CRUDControllerBase<HivePart>
    {
        public HivePartsController(MyHiveDbContext context, ILogger<HivePartsController> logger)
            : base(context, context.HiveParts, logger)
        {
        }
    }
}
