using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StimulusAPI.Context;
using StimulusAPI.Models;

namespace StimulusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodesController : ControllerBase
    {
        private readonly TestStimulusProjet_Evolution _context;

        public CodesController(TestStimulusProjet_Evolution context)
        {
            _context = context;
        }

        // GET: api/Codes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Code>>> GetCodes()
        {
            return await _context.Codes.ToListAsync();
        }

        // GET: api/Codes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Code>> GetCode(int id)
        {
            var code = await _context.Codes.FindAsync(id);

            if (code == null)
            {
                log.Warning($"INVALID ID -> GetCode(int id = {id}) : GET REQUEST Le code est null : Code = {_context.Codes}");

                return NotFound();
            }

            return code;
        }

        // PUT: api/Codes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCode(int id, Code code)
        {
            if (id != code.Id)
            {
                log.Warning($"INVALID ID -> PutCode(int id = {id}, Code code = {code}) : PUT REQUEST L'id fourni ne correspond pas au code : {id} != {code.Id}");

                return BadRequest();
            }

            _context.Entry(code).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CodeExists(id))
                {
                    log.Warning($"INVALID ID -> PutCode(int id = {id}, Code code = {code}) : PUT REQUEST L'id fourni ne correspond à aucun code");

                    return NotFound();
                }
                else
                {
                    log.Error($"ERROR -> PutCode(int id = {id}, Code code = {code}) : PUT REQUEST THROWING ERROR");

                    throw;
                }
            }
            log.Warning($"NO CONTENT -> PutCode(int id = {id}, Code code = {code}) : PUT REQUEST aucun contenu, aucun changement possible");

            return NoContent();
        }

        // POST: api/Codes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Code>> PostCode(Code code)
        {
            _context.Codes.Add(code);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCode", new { id = code.Id }, code);
        }

        // DELETE: api/Codes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCode(int id)
        {
            var code = await _context.Codes.FindAsync(id);
            if (code == null)
            {
                log.Warning($"NULL PARAMETER -> DeleteCode(int id = {id}) : DELETE REQUEST Codes = {code}) Le code est null et donc ne peut pas être effacé");

                return NotFound();
            }

            _context.Codes.Remove(code);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CodeExists(int id)
        {
            return _context.Codes.Any(e => e.Id == id);
        }
    }
}
