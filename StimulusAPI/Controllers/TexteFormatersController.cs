﻿using System;
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
        private readonly DevProjetStimulusContext _context;

        public TexteFormatersController(DevProjetStimulusContext context)
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
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

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
                    return Conflict();
                }
                else
                {
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
