//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using StimulusAPI.Context;
//using StimulusAPI.Models;

//namespace StimulusAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TheorieComposantsController : ControllerBase
//    {
//        private readonly DevProjetStimulusContext _context;

//        public TheorieComposantsController(DevProjetStimulusContext context)
//        {
//            _context = context;
//        }

//        // GET: api/TheorieComposants
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<TheorieComposant>>> GetTheorieComposants()
//        {
//            return await _context.TheorieComposants.ToListAsync();
//        }

//        // GET: api/TheorieComposants/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<TheorieComposant>> GetTheorieComposant(int id)
//        {
//            var theorieComposant = await _context.TheorieComposants.FindAsync(id);

//            if (theorieComposant == null)
//            {
//                return NotFound();
//            }

//            return theorieComposant;
//        }

//        // PUT: api/TheorieComposants/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutTheorieComposant(int id, TheorieComposant theorieComposant)
//        {
//            if (id != theorieComposant.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(theorieComposant).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!TheorieComposantExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/TheorieComposants
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<TheorieComposant>> PostTheorieComposant(TheorieComposant theorieComposant)
//        {
//            _context.TheorieComposants.Add(theorieComposant);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetTheorieComposant", new { id = theorieComposant.Id }, theorieComposant);
//        }

//        // DELETE: api/TheorieComposants/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteTheorieComposant(int id)
//        {
//            var theorieComposant = await _context.TheorieComposants.FindAsync(id);
//            if (theorieComposant == null)
//            {
//                return NotFound();
//            }

//            _context.TheorieComposants.Remove(theorieComposant);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool TheorieComposantExists(int id)
//        {
//            return _context.TheorieComposants.Any(e => e.Id == id);
//        }
//    }
//}
