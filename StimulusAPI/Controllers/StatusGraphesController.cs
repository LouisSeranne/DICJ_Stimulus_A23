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
    public class StatusGraphesController : ControllerBase
    {
        private readonly TestStimulusProjet_Evolution _context;

        public StatusGraphesController(TestStimulusProjet_Evolution context)
        {
            _context = context;
        }

        // GET: api/StatusGraphes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusGraphe>>> GetStatusGraphes()
        {
            return await _context.StatusGraphes.ToListAsync();
        }

        // GET: api/StatusGraphes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StatusGraphe>> GetStatusGraphe(string id)
        {
            var statusGraphe = await _context.StatusGraphes.FindAsync(id);

            if (statusGraphe == null)
            {
                log.Warning($"NULL PARAMETER -> GetStatusGraphe(string id = {id}): GET REQUEST statusGraphe est null");

                return NotFound();
            }

            return statusGraphe;
        }

        // PUT: api/StatusGraphes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatusGraphe(string id, StatusGraphe statusGraphe)
        {
            if (id != statusGraphe.Code)
            {
                log.Warning($"INVALID ID -> PutStatusGraphe(string id = {id}, StatusGraphe statusGraphe = {statusGraphe}): PUT REQUEST L'id ne correspond pas à statusGraphe.Code: {id} != {statusGraphe.Code}");

                return BadRequest();
            }

            _context.Entry(statusGraphe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusGrapheExists(id))
                {
                    log.Warning($"INVALID ID -> PutStatusGraphe(string id = {id}, StatusGraphe statusGraphe = {statusGraphe}): PUT REQUEST L'id ne correspond à aucun statusGraphe.Code");

                    return NotFound();
                }
                else
                {
                    log.Error($"ERROR -> PutStatusGraphe(string id = {id}, StatusGraphe statusGraphe = {statusGraphe}): PUT REQUEST THROWING ERROR");

                    throw;
                }
            }
            log.Warning($"NO CONTENT -> PutStatusGraphe(string id = {id}, StatusGraphe statusGraphe = {statusGraphe}): PUT REQUEST Aucun contenu, aucun changement possible");

            return NoContent();
        }

        // POST: api/StatusGraphes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StatusGraphe>> PostStatusGraphe(StatusGraphe statusGraphe)
        {
            _context.StatusGraphes.Add(statusGraphe);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StatusGrapheExists(statusGraphe.Code))
                {
                    log.Warning($"CONFLICT -> PostStatusGraphe(StatusGraphe statusGraphe = {statusGraphe}): POST REQUEST Un StatusGraphes dont l'id = {statusGraphe.Code} existe déjà");

                    return Conflict();
                }
                else
                {
                    log.Error($"ERROR -> PostStatusGraphe(StatusGraphe statusGraphe = {statusGraphe}): POST REQUEST THROWING ERROR");

                    throw;
                }
            }

            return CreatedAtAction("GetStatusGraphe", new { id = statusGraphe.Code }, statusGraphe);
        }

        // DELETE: api/StatusGraphes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatusGraphe(string id)
        {
            var statusGraphe = await _context.StatusGraphes.FindAsync(id);
            if (statusGraphe == null)
            {
                log.Warning($"NULL PARAMETER-> DeleteStatusGraphe(string id = {id}): DELETE REQUEST statusGraphe est null");

                return NotFound();
            }

            _context.StatusGraphes.Remove(statusGraphe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StatusGrapheExists(string id)
        {
            return _context.StatusGraphes.Any(e => e.Code == id);
        }
    }
}
