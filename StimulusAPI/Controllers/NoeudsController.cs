﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StimulusAPI.Context;
using StimulusAPI.Models;
using System.Reflection;
using Newtonsoft.Json;

namespace StimulusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoeudsController : ControllerBase
    {
        private readonly DevProjetStimulusContext _context;

        public NoeudsController(DevProjetStimulusContext context)
        {
            _context = context;
        }

        // GET: api/Noeuds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Noeud>>> GetNoeuds()
        {
            return await _context.Noeuds.ToListAsync();
        }

        // GET: api/Noeuds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Noeud>> GetNoeud(int id)
        {
            var noeud = await _context.Noeuds.FindAsync(id);

            if (noeud == null)
            {
                return NotFound();
            }

            return noeud;
        }

        // PUT: api/Noeuds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> PutNoeud(int id, Noeud noeud)
        {
            if (id != noeud.Id)
            {
                return BadRequest();
            }
            Noeud noeudOrigin = _context.Noeuds.AsNoTracking().Where(n => n.Id == id).FirstOrDefault();

            foreach(PropertyInfo pi in noeud.GetType().GetProperties())
            {
                if(pi.GetValue(noeud) == null)
                {
                    pi.SetValue(noeud, pi.GetValue(noeudOrigin));
                }
            }

            _context.Entry(noeud).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoeudExists(id))
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

        // POST: api/Noeuds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Noeud>> PostNoeud(Noeud noeud)
        {
            _context.Noeuds.Add(noeud);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNoeud", new { id = noeud.Id }, noeud);
        }

        // DELETE: api/Noeuds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNoeud(int id)
        {
            var noeud = await _context.Noeuds.FindAsync(id);
            if (noeud == null)
            {
                return NotFound();
            }

            _context.Noeuds.Remove(noeud);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NoeudExists(int id)
        {
            return _context.Noeuds.Any(e => e.Id == id);
        }
    }
}
