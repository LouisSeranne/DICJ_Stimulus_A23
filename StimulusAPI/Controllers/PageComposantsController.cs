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
    public class PageComposantsController : ControllerBase
    {
        private readonly TestStimulusProjet_Evolution _context;

        public PageComposantsController(TestStimulusProjet_Evolution context)
        {
            _context = context;
        }

        // GET: api/PageComposants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PageComposant>>> GetPageComposants()
        {
            return await _context.PageComposants.ToListAsync();
        }

        // GET: api/PageComposants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PageComposant>> GetPageComposant(int id)
        {
            var pageComposant = await _context.PageComposants.FindAsync(id);

            if (pageComposant == null)
            {
                return NotFound();
            }

            return pageComposant;
        }

        // PUT: api/PageComposants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPageComposant(int id, PageComposant pageComposant)
        {
            if (id != pageComposant.Id)
            {
                return BadRequest();
            }

            _context.Entry(pageComposant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PageComposantExists(id))
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

        // POST: api/PageComposants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PageComposant>> PostPageComposant(PageComposant pageComposant)
        {
            _context.PageComposants.Add(pageComposant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPageComposant", new { id = pageComposant.Id }, pageComposant);
        }

        // DELETE: api/PageComposants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePageComposant(int id)
        {
            var pageComposant = await _context.PageComposants.FindAsync(id);
            if (pageComposant == null)
            {
                return NotFound();
            }

            _context.PageComposants.Remove(pageComposant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PageComposantExists(int id)
        {
            return _context.PageComposants.Any(e => e.Id == id);
        }
    }
}
