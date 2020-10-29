using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyHiveService.Models;

namespace MyHiveService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HivesController : CRUDControllerBase<Hive>
    {
        public HivesController(MyHiveDbContext context, ILogger<HivesController> logger)
            : base(context, context.Hives, logger)
        {
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

    }
}
