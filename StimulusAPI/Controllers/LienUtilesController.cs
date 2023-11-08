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
    public class LienUtilesController : ControllerBase
    {
        private readonly TestStimulusProjet_Evolution _context;

        public LienUtilesController(TestStimulusProjet_Evolution context)
        {
            _context = context;
        }

        // GET: api/LienUtiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LienUtileVM>>> GetLienUtiles()
        {
            var lienUtile = await _context.LienUtiles.Include(l => l.Graphe).ToListAsync();

            List<LienUtileVM> result = new List<LienUtileVM>();

            foreach (LienUtile lien in lienUtile)
            {
                LienUtileVM lienUtileVM = new LienUtileVM(lien);
                result.Add(lienUtileVM);
                lien.Graphe.LienUtiles = null;
            }
            return result;

        }

        // GET: api/LienUtiles/5
        [HttpGet("{id}")]
        public async Task<List<LienUtileVM>> GetLienUtile(int id)
        {
            var lienUtile = await _context.LienUtiles.Where(x =>x.GrapheId == id).ToListAsync();

            List<LienUtileVM> lienUtileVMs = new List<LienUtileVM>();

            foreach(LienUtile lien in lienUtile)
            {
                LienUtileVM lienUtileVM = new LienUtileVM(lien);
                lienUtileVMs.Add(lienUtileVM);
            }

            return lienUtileVMs;
        }

        // PUT: api/LienUtiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLienUtile(int id, LienUtile lienUtile)
        {
            if (id != lienUtile.Id)
            {
                return BadRequest();
            }

            _context.Entry(lienUtile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LienUtileExists(id))
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

        // POST: api/LienUtiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LienUtile>> PostLienUtile(LienUtile lienUtile)
        {
            _context.LienUtiles.Add(lienUtile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLienUtile", new { id = lienUtile.Id }, lienUtile);
        }

        // DELETE: api/LienUtiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLienUtile(int id)
        {
            var lienUtile = await _context.LienUtiles.FindAsync(id);
            if (lienUtile == null)
            {
                return NotFound();
            }

            _context.LienUtiles.Remove(lienUtile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LienUtileExists(int id)
        {
            return _context.LienUtiles.Any(e => e.Id == id);
        }
    }
}
