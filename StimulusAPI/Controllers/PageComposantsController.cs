using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
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
            var log = Log.ForContext<StimulusAPI.Controllers.PageComposantsController>();
            log.Information($"GetPageComposants(): Context : {_context}");

            return await _context.PageComposants.ToListAsync();
        }

        // GET: api/PageComposants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PageComposant>> GetPageComposant(int id)
        {
            var log = Log.ForContext<StimulusAPI.Controllers.PageComposantsController>();

            var pageComposant = await _context.PageComposants.FindAsync(id);

            if (pageComposant == null)
            {
                log.Information($"NULL PARAMETER -> GetPageComposant(int id = {id}): GET REQUEST Le pageComposant = {pageComposant} est null");

                return NotFound();
            }

            return pageComposant;
        }

        // PUT: api/PageComposants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPageComposant(int id, PageComposant pageComposant)
        {
            var log = Log.ForContext<StimulusAPI.Controllers.PageComposantsController>();

            if (id != pageComposant.Id)
            {
                log.Information($"INVALID ID -> PutPageComposant(int id = {id}, PageComposant pageComposant = {pageComposant}): PUT REQUEST L'id ne correspond pas à l'id de pageComposant: {id} != {pageComposant}");

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
                    log.Information($"INVALID ID -> PutPageComposant(int id = {id}, PageComposant pageComposant = {pageComposant}): PUT REQUEST L'id ne correspond à aucun pageComposant");

                    return NotFound();
                }
                else
                {
                    log.Information($"ERROR -> PutPageComposant(int id = {id}, PageComposant pageComposant = {pageComposant}): PUT REQUEST THROWING ERROR");

                    throw;
                }
            }
            log.Information($"NO CONTENT -> PutPageComposant(int id = {id}, PageComposant pageComposant = {pageComposant}): PUT REQUEST Aucun contenu, aucun changement possible");

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
            var log = Log.ForContext<StimulusAPI.Controllers.PageComposantsController>();

            var pageComposant = await _context.PageComposants.FindAsync(id);
            if (pageComposant == null)
            {
                log.Information($"NULL PARAMETER -> DeletePageComposant(int id = {id}): DELETE REQUEST L'id est null");

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
