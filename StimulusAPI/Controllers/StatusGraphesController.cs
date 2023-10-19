using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StimulusAPI.Context;
using StimulusAPI.Models;

namespace StimulusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusGraphesController : ControllerBase
    {
        private readonly TestStimulusProjet_Evolution _context;

        public StatusGraphesController(TestStimulusProjet_Evolution context)
        {
            _context = context;
        }

        // GET: api/StatusGraphes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusGraphe>>> GetStatusGraphes()
        {
            return await _context.StatusGraphes.ToListAsync();
        }

        // GET: api/StatusGraphes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StatusGraphe>> GetStatusGraphe(string id)
        {
            var statusGraphe = await _context.StatusGraphes.FindAsync(id);

            if (statusGraphe == null)
            {
                return NotFound();
            }

            return statusGraphe;
        }

        // PUT: api/StatusGraphes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatusGraphe(string id, StatusGraphe statusGraphe)
        {
            if (id != statusGraphe.Code)
            {
                return BadRequest();
            }

            _context.Entry(statusGraphe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusGrapheExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StatusGraphes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StatusGraphe>> PostStatusGraphe(StatusGraphe statusGraphe)
        {
            _context.StatusGraphes.Add(statusGraphe);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StatusGrapheExists(statusGraphe.Code))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStatusGraphe", new { id = statusGraphe.Code }, statusGraphe);
        }

        // DELETE: api/StatusGraphes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatusGraphe(string id)
        {
            var statusGraphe = await _context.StatusGraphes.FindAsync(id);
            if (statusGraphe == null)
            {
                return NotFound();
            }

            _context.StatusGraphes.Remove(statusGraphe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StatusGrapheExists(string id)
        {
            return _context.StatusGraphes.Any(e => e.Code == id);
        }
    }
}
