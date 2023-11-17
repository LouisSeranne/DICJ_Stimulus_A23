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
    public class TexteFormatersController : ControllerBase
    {
        private readonly TestStimulusProjet_Evolution _context;

        public TexteFormatersController(TestStimulusProjet_Evolution context)
        {
            _context = context;
        }

        // GET: api/TexteFormaters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TexteFormater>>> GetTexteFormaters()
        {
            return await _context.TexteFormaters.ToListAsync();
        }

        // GET: api/TexteFormaters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TexteFormater>> GetTexteFormater(int id)
        {
            var texteFormater = await _context.TexteFormaters.FindAsync(id);

            if (texteFormater == null)
            {
                log.Warning($"NULL PARAMETER -> GetTexteFormater(int id = {id}): GET REQUEST texteFormater est null");

                return NotFound();
            }

            return texteFormater;
        }

        // PUT: api/TexteFormaters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTexteFormater(int id, TexteFormater texteFormater)
        {
            if (id != texteFormater.Id)
            {
                log.Warning($"INVALID ID -> PutTexteFormater(int id = {id}, TexteFormater texteFormater = {texteFormater}): PUT REQUEST L'id ne correspond pas à texteFormater.Id: {id} != {texteFormater.Id}");

                return BadRequest();
            }

            _context.Entry(texteFormater).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TexteFormaterExists(id))
                {
                    log.Warning($"INVALID ID -> PutTexteFormater(int id = {id}, TexteFormater texteFormater = {texteFormater}): PUT REQUEST L'id ne correspond à aucun texteFormater.Id");

                    return NotFound();
                }
                else
                {
                    log.Error($"ERROR -> PutTexteFormater(int id = {id}, TexteFormater texteFormater = {texteFormater}): PUT REQUEST THROWING ERROR");

                    throw;
                }
            }
            log.Warning($"NO CONTENT -> PutTexteFormater(int id = {id}, TexteFormater texteFormater = {texteFormater}): PUT REQUEST Aucun contenu, aucun changement possible");

            return NoContent();
        }

        // POST: api/TexteFormaters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TexteFormater>> PostTexteFormater(TexteFormater texteFormater)
        {
            _context.TexteFormaters.Add(texteFormater);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TexteFormaterExists(texteFormater.Id))
                {
                    log.Warning($"CONFLICT -> PostTexteFormater(TexteFormater texteFormater = {texteFormater}): POST REQUEST Un texteFormater dont l'id = {texteFormater.Id} existe déjà");

                    return Conflict();
                }
                else
                {
                    log.Error($"ERROR -> PostTexteFormater(TexteFormater texteFormater = {texteFormater}): POST REQUEST THROWING ERROR");

                    throw;
                }
            }

            return CreatedAtAction("GetTexteFormater", new { id = texteFormater.Id }, texteFormater);
        }

        // DELETE: api/TexteFormaters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTexteFormater(int id)
        {
            var texteFormater = await _context.TexteFormaters.FindAsync(id);
            if (texteFormater == null)
            {
                log.Warning($"NULL PARAMETER -> DeleteTexteFormater(int id = {id}): DELETE REQUEST texteFormater est null");

                return NotFound();
            }

            _context.TexteFormaters.Remove(texteFormater);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TexteFormaterExists(int id)
        {
            return _context.TexteFormaters.Any(e => e.Id == id);
        }
    }
}
