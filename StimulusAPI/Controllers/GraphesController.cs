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
    public class GraphesController : ControllerBase
    {
        private readonly TestStimulusProjet_Evolution _context;

        public GraphesController(TestStimulusProjet_Evolution context)
        {
            _context = context;
        }

        // GET: api/Graphes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GrapheVM>>> GetGraphes()
        {
            List<Graphe> response  = await _context.Graphes.ToListAsync();
            List<GrapheVM> retour = new List<GrapheVM>();
            foreach(Graphe graphe in response)
            {
                retour.Add(new GrapheVM(graphe));
            }
            return retour;
            
        }

        // GET: api/Graphes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GrapheVM>> GetGraphe(int id)
        {
            var graphe = await _context.Graphes.FindAsync(id);

            if (graphe == null)
            {
                log.Warning($" NULL PARAMETER -> GetGraphe(int id = {id}): GET REQUEST Le graphe est null ");

                return NotFound();
            }

            var response = new GrapheVM(graphe);

            return response;
        }

        // PUT: api/Graphes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGraphe(int id, Graphe graphe)
        {
            if (id != graphe.Id)
            {
                log.Warning($" INVALID ID -> PutGraphe(int id = {id}, Graphe graphe = {graphe}): PUT REQUEST L'id ne correspond pas au graphe: {id} =! {graphe.Id} ");

                return BadRequest();
            }

            _context.Entry(graphe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GrapheExists(id))
                {
                    log.Warning($" INVALID ID -> PutGraphe(int id = {id}, Graphe graphe = {graphe}): PUT REQUEST L'id ne correspond à aucun graphe");

                    return NotFound();
                }
                else
                {
                    log.Error($" ERROR -> PutGraphe(int id = {id}, Graphe graphe = {graphe}): PUT REQUEST THROWING ERROR");

                    throw;
                }
            }
            log.Warning($" NO CONTENT -> PutGraphe(int id = {id}, Graphe graphe = {graphe}): PUT REQUEST Aucun contenu, aucune modification possible");

            return NoContent();
        }

        // POST: api/Graphes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Graphe>> PostGraphe(Graphe graphe)
        {
            _context.Graphes.Add(graphe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGraphe", new { id = graphe.Id }, graphe);
        }

        // DELETE: api/Graphes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGraphe(int id)
        {
            var graphe = await _context.Graphes.FindAsync(id);
            if (graphe == null)
            {
                log.Warning($"NULL PARAMETER -> DeleteGraphe(int id = {id}): DELETE REQUEST Le graphe est null");

                return NotFound();
            }

            _context.Graphes.Remove(graphe);
            await _context.SaveChangesAsync();


            return NoContent();
        }

        private bool GrapheExists(int id)
        {
            return _context.Graphes.Any(e => e.Id == id);
        }
    }
}
