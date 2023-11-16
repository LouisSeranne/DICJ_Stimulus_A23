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
    public class ReferencesController : ControllerBase
    {
        private readonly TestStimulusProjet_Evolution _context;

        public ReferencesController(TestStimulusProjet_Evolution context)
        {
            _context = context;
        }

        // GET: api/References
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reference>>> GetReferences()
        {
            var log = Log.ForContext<StimulusAPI.Controllers.ReferencesController>();
            log.Information($"GetReferences(): Context: {_context}");

            return await _context.References.ToListAsync();
        }

        // GET: api/References/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reference>> GetReference(int id)
        {
            var log = Log.ForContext<StimulusAPI.Controllers.ReferencesController>();

            var reference = await _context.References.FindAsync(id);

            if (reference == null)
            {
                log.Warning($"NULL PARAMETER -> GetReference(int id = {id}): GET REQUEST reference est null");

                return NotFound();
            }

            return reference;
        }

        // PUT: api/References/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReference(int id, Reference reference)
        {
            var log = Log.ForContext<StimulusAPI.Controllers.ReferencesController>();

            if (id != reference.Id)
            {
                log.Warning($"INVALID ID -> PutReference(int id = {id}, Reference reference = {reference}): PUT REQUEST L'id ne correspond pas à l'id de reference: {id} != {reference.Id}");

                return BadRequest();
            }

            _context.Entry(reference).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReferenceExists(id))
                {
                    log.Warning($"INVALID ID -> PutReference(int id = {id}, Reference reference = {reference}): PUT REQUEST L'id ne correspond à aucun id de reference");

                    return NotFound();
                }
                else
                {
                    log.Error($"ERROR -> PutReference(int id = {id}, Reference reference = {reference}): PUT REQUEST THROWING ERROR");

                    throw;
                }
            }
            log.Warning($"NO CONTENT -> PutReference(int id = {id}, Reference reference = {reference}): PUT REQUEST Aucun contenu, aucun changement possible");

            return NoContent();
        }

        // POST: api/References
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reference>> PostReference(Reference reference)
        {
            _context.References.Add(reference);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReference", new { id = reference.Id }, reference);
        }

        // DELETE: api/References/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReference(int id)
        {
            var log = Log.ForContext<StimulusAPI.Controllers.ReferencesController>();

            var reference = await _context.References.FindAsync(id);
            if (reference == null)
            {
                log.Warning($"NULL PARAMETER -> DeleteReference(int id = {id}): DELETE REQUEST reference est null");

                return NotFound();
            }

            _context.References.Remove(reference);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReferenceExists(int id)
        {
            return _context.References.Any(e => e.Id == id);
        }
    }
}
