using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyHiveService.Models;
using MyHiveService.Models.DB;
using MyHiveService.Services.Download;

namespace MyHiveService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HivesController : CRUDControllerBase<Hive>
    {
        private readonly IDownloadService _downloadService;

        public HivesController(MyHiveDbContext context, ILogger<HivesController> logger, IDownloadService downloadService)
            : base(context, context.Hives, logger)
        {
            _downloadService = downloadService;
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<Hive>> Get(Guid id)
        {
            Hive entity = _context.Hives
                .Where(h => h.id == id)
                .Include(h => h.parts)
                    .ThenInclude(p => p.frames)
                        .ThenInclude(f => f.inspections)
                .Include(h => h.parts)
                        .ThenInclude(p => p.inspections)
                .Include(h => h.inspections)
                .FirstOrDefault();
            if (entity == null)
            {
                return NotFound();
            }
            return entity;
        }

        [HttpGet("download")]
        
        public ActionResult Download()
        {
            byte[] zip = _downloadService.downloadZip();
            string fileName = DateTime.UtcNow.ToString("o");
            return File(zip, System.Net.Mime.MediaTypeNames.Application.Zip, $"hivedata_{fileName}.zip");
        }
    }
}
