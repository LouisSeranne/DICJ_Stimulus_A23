using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StimulusAPI.Context;
using StimulusAPI.Models;
using StimulusAPI.ViewModels;

namespace StimulusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EtudiantsController : ControllerBase
    {
        private readonly TestStimulusProjet_Evolution _context;

        public EtudiantsController(TestStimulusProjet_Evolution context)
        {
            _context = context;
        }

        // GET: api/Etudiants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Etudiant>>> GetEtudiants()
        {
            Console.WriteLine("Get Etudiant (API)");

            return await _context.Etudiants.ToListAsync();
        }

        // GET: api/Etudiants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EtudiantVM>> GetEtudiant(string id)
        {
            var etudiant =  _context.Etudiants.Where(e => e.CodeDa == id).Include(e => e.Groupes).FirstOrDefault();


            if (etudiant == null)
            {
                return NotFound();
            }

            var response = new EtudiantVM(etudiant);

            Console.WriteLine("Get Etudiant Id (API)");

            return response;
        }

        // PUT: api/Etudiants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEtudiant(string id, Etudiant etudiant)
        {
            if (id != etudiant.CodeDa)
            {
                return BadRequest();
            }

            _context.Entry(etudiant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EtudiantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Console.WriteLine("Put Etudiant (API)");

            return NoContent();
        }

        // POST: api/Etudiants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Etudiant>> PostEtudiant(Etudiant etudiant)
        {
            _context.Etudiants.Add(etudiant);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EtudiantExists(etudiant.CodeDa))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            Console.WriteLine("Post Etudiant (API)");

            return CreatedAtAction("GetEtudiant", new { id = etudiant.CodeDa }, etudiant);
        }

        // DELETE: api/Etudiants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEtudiant(string id)
        {
            var etudiant = await _context.Etudiants.FindAsync(id);
            if (etudiant == null)
            {
                return NotFound();
            }

            _context.Etudiants.Remove(etudiant);
            await _context.SaveChangesAsync();

            Console.WriteLine("Delete Etudiant (API)");

            return NoContent();
        }

        private bool EtudiantExists(string id)
        {
            return _context.Etudiants.Any(e => e.CodeDa == id);
        }
    }
}
