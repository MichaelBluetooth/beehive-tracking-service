using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyHiveService.Models.DB;
using MyHiveService.Models.DTO;
using MyHiveService.Services;

namespace MyHiveService.Controllers
{
    [ApiController]
    [Route("api/sync")]
    [Authorize]
    public class SyncController : ControllerBase
    {
        private readonly MyHiveDbContext _context;
        private readonly ILogger<HivesController> _logger;
        private readonly ISyncService _syncService;

        public SyncController(MyHiveDbContext context, ILogger<HivesController> logger, ISyncService syncService)
        {
            _context = context;
            _logger = logger;
            _syncService = syncService;
        }

        [HttpPost]
        [Route("hive")]
        public async Task<ActionResult<Hive>> SyncHive(HiveDTO hive)
        {
            _logger.LogInformation("Syncing hive", hive);
            Hive syncd = _syncService.syncHive(hive);
            await _context.SaveChangesAsync();
            return Ok(syncd);
        }

        [HttpPost]
        [Route("body")]
        public async Task<ActionResult<HivePart>> SyncBody(HivePart body)
        {
            _logger.LogInformation("Syncing body", body);
            HivePart syncd = _syncService.syncPart(body);
            await _context.SaveChangesAsync();
            return Ok(syncd);
        }

        [HttpPost] 
        [Route("frame")]
        public async Task<ActionResult<Frame>> SyncFrame(Frame frame)
        {
            _logger.LogInformation("Syncing frame", frame);
            Frame syncd = _syncService.syncFrame(frame);
            await _context.SaveChangesAsync();
            return Ok(syncd);
        }

        [HttpPost]
        [Route("hiveinspection")]
        public async Task<ActionResult<HiveInspection>> SyncHiveInspection(HiveInspectionDTO inspection)
        {
            _logger.LogInformation("Syncing hive inspection", inspection);
            HiveInspection syncd = _syncService.syncInspection(inspection);
            await _context.SaveChangesAsync();
            return Ok(syncd);
        }

        [HttpPost]
        [Route("bodyinspection")]
        public async Task<ActionResult<HivePartInspection>> SyncBodyInspection(HivePartInspectionDTO inspection)
        {
            _logger.LogInformation("Syncing hive body inspection", inspection);
            HivePartInspection syncd = _syncService.syncInspection(inspection);
            await _context.SaveChangesAsync();
            return Ok(syncd);
        }

        [HttpPost]
        [Route("frameinspection")]
        public async Task<ActionResult<FrameInspection>> SyncFrameInspection(FrameInspectionDTO inspection)
        {
            _logger.LogInformation("Syncing frame inspection", inspection);
            FrameInspection syncd = _syncService.syncInspection(inspection);
            await _context.SaveChangesAsync();
            return Ok(syncd);
        }
    }
}
