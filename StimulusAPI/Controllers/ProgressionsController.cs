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
            var log = Log.ForContext<StimulusAPI.Controllers.ProgressionsController>();
            log.Information($"GetProgressions(): Context: {_context}");

            return await _context.Progressions.ToListAsync();
        }

        // GET: api/Progressions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Progression>> GetProgression(int id)
        {
            var log = Log.ForContext<StimulusAPI.Controllers.ProgressionsController>();

            var progression = await _context.Progressions.FindAsync(id);

            if (progression == null)
            {
                log.Information($"NULL PARAMETER -> GetProgression(int id = {id}): GET REQUEST progression est null");

                return NotFound();
            }

            return progression;
        }

        // PUT: api/Progressions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProgression(int id, Progression progression)
        {
            var log = Log.ForContext<StimulusAPI.Controllers.ProgressionsController>();

            if (id != progression.PageId)
            {
                log.Warning($"INVALID ID -> PutProgression(int id = {id}, Progression progression = {progression}): PUT REQUEST L'id ne correspond pas à l'id de progression: {id} != {progression.PageId}");

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
                    log.Warning($"INVALID ID -> PutProgression(int id = {id}, Progression progression = {progression}): PUT REQUEST L'id ne correspond à aucun id de progression");

                    return NotFound();
                }
                else
                {
                    log.Error($"ERROR -> PutProgression(int id = {id}, Progression progression = {progression}): PUT REQUEST THROWING ERROR");

                    throw;
                }
            }
            log.Warning($"NO CONTENT -> PutProgression(int id = {id}, Progression progression = {progression}): PUT REQUEST Aucun contenu, aucun changement possible");

            return NoContent();
        }

        // POST: api/Progressions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Progression>> PostProgression(Progression progression)
        {
            var log = Log.ForContext<StimulusAPI.Controllers.ProgressionsController>();

            _context.Progressions.Add(progression);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProgressionExists(progression.PageId))
                {
                    log.Warning($"CONFLICT -> PostProgression(Progression progression ={progression}): POST REQUEST Un élément progression dont l'id = {progression.PageId} existe déjà");

                    return Conflict();
                }
                else
                {
                    log.Error($"ERROR -> PostProgression(Progression progression ={progression}): POST REQUEST THROWING ERROR");

                    throw;
                }
            }

            return CreatedAtAction("GetProgression", new { id = progression.PageId }, progression);
        }

        // DELETE: api/Progressions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgression(int id)
        {
            var log = Log.ForContext<StimulusAPI.Controllers.ProgressionsController>();

            var progression = await _context.Progressions.FindAsync(id);
            if (progression == null)
            {
                log.Warning($"NULL PARAMETER -> DeleteProgression(int id = {id}): DELETE REQUEST progression est null");

                return NotFound();
            }

            _context.Progressions.Remove(progression);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProgressionExists(int id)
        {
            return _context.Progressions.Any(e => e.PageId == id);
        }
    }
}
