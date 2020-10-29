using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyHiveService.Models;

namespace MyHiveService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FrameInspectionsController : CRUDControllerBase<FrameInspection>
    {
        public FrameInspectionsController(MyHiveDbContext context, ILogger<FrameInspectionsController> logger)
            : base(context, context.FrameInspections, logger)
        {
        }
    }
}
