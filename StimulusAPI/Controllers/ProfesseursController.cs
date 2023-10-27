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
    public class ProfesseursController : ControllerBase
    {
        private readonly TestStimulusProjet_Evolution _context;

        public ProfesseursController(TestStimulusProjet_Evolution context)
        {
            _context = context;
        }

        // GET: api/Professeurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Professeur>>> GetProfesseurs()
        {
            Console.WriteLine("Get Professeurs (API)");
            
            return await _context.Professeurs.ToListAsync();
        }

        // GET: api/Professeurs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Professeur>> GetProfesseur(int id)
        {
            var professeur = await _context.Professeurs.FindAsync(id);

            if (professeur == null)
            {
                return NotFound();
            }

            Console.WriteLine("Get Professeur Id (API)");

            return professeur;
        }

        // PUT: api/Professeurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfesseur(int id, Professeur professeur)
        {
            if (id != professeur.Id)
            {
                return BadRequest();
            }

            _context.Entry(professeur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesseurExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Console.WriteLine("Put Professeur (API)");

            return NoContent();
        }

        // POST: api/Professeurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Professeur>> PostProfesseur(Professeur professeur)
        {
            _context.Professeurs.Add(professeur);
            await _context.SaveChangesAsync();

            Console.WriteLine("Post Professeur (API)");

            return CreatedAtAction("GetProfesseur", new { id = professeur.Id }, professeur);
        }

        // DELETE: api/Professeurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesseur(int id)
        {
            var professeur = await _context.Professeurs.FindAsync(id);
            if (professeur == null)
            {
                return NotFound();
            }

            _context.Professeurs.Remove(professeur);
            await _context.SaveChangesAsync();

            Console.WriteLine("Delete Professeur");

            return NoContent();
        }

        private bool ProfesseurExists(int id)
        {
            return _context.Professeurs.Any(e => e.Id == id);
        }
    }
}
