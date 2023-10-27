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
    public class GroupesController : ControllerBase
    {
        private readonly TestStimulusProjet_Evolution _context;

        public GroupesController(TestStimulusProjet_Evolution context)
        {
            _context = context;
        }

        // GET: api/Groupes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupeVM>>> GetGroupes()
        {
            List<Groupe> groupes = await _context.Groupes.ToListAsync();
            List<GroupeVM> response = new List<GroupeVM>();
            foreach(Groupe groupe in groupes)
            {
                response.Add(new GroupeVM(groupe));
            }

            Console.WriteLine("Get Groupes (API)");

            return response;

        }

        // GET: api/Groupes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupeVM>> GetGroupe(int id)
        {
            var groupe = _context.Groupes.Include(g => g.EtudiantDa).Where(g => g.Id == id).FirstOrDefault();

            if (groupe == null)
            {
                return NotFound();
            }
            var response = new GroupeVM(groupe);

            Console.WriteLine("Get Groupe Id (API)");

            return response;
        }

        // PUT: api/Groupes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroupe(int id, Groupe groupe)
        {
            if (id != groupe.Id)
            {
                return BadRequest();
            }

            _context.Entry(groupe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Console.WriteLine("Put Groupe (API)");

            return NoContent();
        }

        // POST: api/Groupes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<ActionResult<Groupe>> PostGroupe(Groupe groupe)
        {
            _context.Groupes.Add(groupe);
            await _context.SaveChangesAsync();

            Console.WriteLine("Post Groupe (API)");

            return CreatedAtAction("GetGroupe", new { id = groupe.Id }, groupe);
        }

        // DELETE: api/Groupes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroupe(int id)
        {
            var groupe = await _context.Groupes.FindAsync(id);
            if (groupe == null)
            {
                return NotFound();
            }

            _context.Groupes.Remove(groupe);
            await _context.SaveChangesAsync();

            Console.WriteLine("Delete Groupe (API)");

            return NoContent();
        }

        private bool GroupeExists(int id)
        {
            return _context.Groupes.Any(e => e.Id == id);
        }
    }
}
