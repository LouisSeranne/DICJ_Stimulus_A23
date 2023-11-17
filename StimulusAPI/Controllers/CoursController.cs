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
    public class CoursController : ControllerBase
    {
        private readonly TestStimulusProjet_Evolution _context;

        public CoursController(TestStimulusProjet_Evolution context)
        {
            _context = context;
        }

        // GET: api/Cours
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cour>>> GetCours()
        {
            return await _context.Cours.ToListAsync();
        }

        // GET: api/Cours/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cour>> GetCour(int id)
        {
            await Task.Delay(500);
            var cour = await _context.Cours.Where(c => c.Id == id).FirstOrDefaultAsync();

            if (cour == null)
            {
                log.Warning($"NULL PARAMETER -> GetCour(int id = {id}) : GET REQUEST cour = {cour}   Le cour est null"); 

                return NotFound();
            }

            return cour;
        }

        // PUT: api/Cours/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCour(int id, Cour cour)
        {
            if (id != cour.Id)
            {
                return BadRequest();
            }

            _context.Entry(cour).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourExists(id))
                {
                    log.Warning($"INVALID ID -> PutCour(int id = {id}, Cour cour = {cour}) : PUT REQUEST L'id fourni ne correspond à aucun cours"); //Watch for type issue, might need to convert ToString()

                    return NotFound();
                }
                else
                {
                    log.Error($"INVALID ID -> PutCour(int id = {id}, Cour cour = {cour}) : PUT REQUEST THROWING ERROR"); //Watch for type issue, might need to convert ToString()

                    throw;
                }
            }

            log.Warning($"NO CONTENT -> PutCour(int id = {id}, Cour cour = {cour}) : PUT REQUEST aucun contenu, aucun changement possible"); //Watch for type issue, might need to convert ToString()

            return NoContent();
        }

        // POST: api/Cours
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cour>> PostCour(Cour cour)
        {
            _context.Cours.Add(cour);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCour", new { id = cour.Id }, cour);
        }

        // DELETE: api/Cours/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCour(int id)
        {
            var cour = await _context.Cours.FindAsync(id);
            if (cour == null)
            {
                log.Warning($"NULL PARAMETER -> DeleteCour(int id = {id}) : DELETE REQUEST cour = {cour} Le cour est null"); //Watch for type issue, might need to convert ToString()

                return NotFound();
            }

            _context.Cours.Remove(cour);
            await _context.SaveChangesAsync();


            return NoContent();
        }

        private bool CourExists(int id)
        {
            return _context.Cours.Any(e => e.Id == id);
        }
    }
}
