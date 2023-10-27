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
    public class ProgressionsController : ControllerBase
    {
        private readonly TestStimulusProjet_Evolution _context;

        public ProgressionsController(TestStimulusProjet_Evolution context)
        {
            _context = context;
        }

        // GET: api/Progressions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Progression>>> GetProgressions()
        {
            Console.WriteLine("Get Progression (API)");

            return await _context.Progressions.ToListAsync();
        }

        // GET: api/Progressions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Progression>> GetProgression(int id)
        {
            var progression = await _context.Progressions.FindAsync(id);

            if (progression == null)
            {
                return NotFound();
            }

            Console.WriteLine("Get Progression Id (API)");

            return progression;
        }

        // PUT: api/Progressions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProgression(int id, Progression progression)
        {
            if (id != progression.PageId)
            {
                return BadRequest();
            }

            _context.Entry(progression).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgressionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Console.WriteLine("Put Progression (API)");

            return NoContent();
        }

        // POST: api/Progressions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Progression>> PostProgression(Progression progression)
        {
            _context.Progressions.Add(progression);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProgressionExists(progression.PageId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            Console.WriteLine("Post Progression (API)");

            return CreatedAtAction("GetProgression", new { id = progression.PageId }, progression);
        }

        // DELETE: api/Progressions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgression(int id)
        {
            var progression = await _context.Progressions.FindAsync(id);
            if (progression == null)
            {
                return NotFound();
            }

            _context.Progressions.Remove(progression);
            await _context.SaveChangesAsync();

            Console.WriteLine("Delete Progression (API)");

            return NoContent();
        }

        private bool ProgressionExists(int id)
        {
            return _context.Progressions.Any(e => e.PageId == id);
        }
    }
}
