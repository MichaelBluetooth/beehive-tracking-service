using System;
using System.Collections.Generic;
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
    [Authorize]
    public abstract class CRUDControllerBase<EntityType> : ControllerBase
        where EntityType : ModelBase, new()
    {
        protected readonly MyHiveDbContext _context;
        protected readonly DbSet<EntityType> _repo;
        protected readonly ILogger _logger;

        public CRUDControllerBase(MyHiveDbContext context, DbSet<EntityType> repo, ILogger logger)
        {
            _context = context;
            _repo = repo;
            _logger = logger;
        }

        private bool ItemExists(Guid id) => _repo.Any(e => e.id == id);

        [HttpPost]
        public async Task<ActionResult<EntityType>> Create(EntityType entity)
        {
            _repo.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = entity.id }, entity);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EntityType>>> GetCollection()
        {
            return await _repo.ToListAsync();
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<EntityType>> Get(Guid id)
        {
            EntityType entity = await _repo.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return entity;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, EntityType entity)
        {
            if (id != entity.id)
            {
                return BadRequest();
            }

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(Get), new { id = entity.id }, entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            EntityType entity = await _repo.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            _repo.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
