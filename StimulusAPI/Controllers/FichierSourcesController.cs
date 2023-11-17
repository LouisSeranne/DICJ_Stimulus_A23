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
    public class FichierSourcesController : ControllerBase
    {
        private readonly TestStimulusProjet_Evolution _context;

        public FichierSourcesController(TestStimulusProjet_Evolution context)
        {
            _context = context;
        }

        // GET: api/FichierSources
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FichierSource>>> GetFichierSources()
        {
            return await _context.FichierSources.ToListAsync();
        }

        // GET: api/FichierSources/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FichierSource>> GetFichierSource(int id)
        {
            var fichierSource = await _context.FichierSources.FindAsync(id);

            if (fichierSource == null)
            {
                log.Warning($"NULL PARAMETER -> GetFichierSource(int id = {id}): GET REQUEST Le fichierSource est null");

                return NotFound();
            }

            return fichierSource;
        }

        // PUT: api/FichierSources/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFichierSource(int id, FichierSource fichierSource)
        {
            if (id != fichierSource.Id)
            {
                log.Warning($"INVALID ID -> PutFichierSource(int id = {id}, FichierSource fichierSource = {fichierSource}): PUT REQUEST L'id fourni ne correspond pas au fichierSource : {id} != {fichierSource}");

                return BadRequest();
            }

            _context.Entry(fichierSource).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FichierSourceExists(id))
                {
                    log.Warning($"INVALID ID -> PutFichierSource(int id = {id}, FichierSource fichierSource = {fichierSource}): PUT REQUEST L'id fourni ne correspond à aucun fichierSource");

                    return NotFound();
                }
                else
                {
                    log.Error($"ERROR -> PutFichierSource(int id = {id}, FichierSource fichierSource = {fichierSource}): PUT REQUEST THROWING ERROR");

                    throw;
                }
            }

            log.Warning($"NO CONTENT -> PutFichierSource(int id = {id}, FichierSource fichierSource = {fichierSource}): PUT REQUEST Aucun contenu, aucun changement possible");

            return NoContent();
        }

        // POST: api/FichierSources
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FichierSource>> PostFichierSource(FichierSource fichierSource)
        {
            _context.FichierSources.Add(fichierSource);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFichierSource", new { id = fichierSource.Id }, fichierSource);
        }

        // DELETE: api/FichierSources/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFichierSource(int id)
        {
            var fichierSource = await _context.FichierSources.FindAsync(id);
            if (fichierSource == null)
            {
                log.Warning($"NULL PARAMETER -> DeleteFichierSource(int id = {id} ): DELETE REQUEST Le fichierSource est null");

                return NotFound();
            }

            _context.FichierSources.Remove(fichierSource);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FichierSourceExists(int id)
        {
            return _context.FichierSources.Any(e => e.Id == id);
        }
    }
}
