using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyHiveService.Models;

namespace MyHiveService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FramesController : CRUDControllerBase<Frame>
    {
        public FramesController(MyHiveDbContext context, ILogger<FramesController> logger)
            : base(context, context.Frames, logger)
        {
        }
    }
}
