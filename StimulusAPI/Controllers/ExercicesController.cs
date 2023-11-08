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
    public class ExercicesController : ControllerBase
    {
        private readonly TestStimulusProjet_Evolution _context;

        public ExercicesController(TestStimulusProjet_Evolution context)
        {
            _context = context;
        }


        // GET: api/Exercices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exercice>>> GetExercices()
        {
            var log = Log.ForContext<StimulusAPI.Controllers.ExercicesController>();
            log.Information($"GetExercices(): Context : {_context}"); 

            return await _context.Exercices.ToListAsync();
        }

        // GET: api/Exercices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Exercice>> GetExercice(int id)
        {
            var log = Log.ForContext<StimulusAPI.Controllers.ExercicesController>();

            var exercice = await _context.Exercices.FindAsync(id);

            if (exercice == null)
            {
                log.Information($"NULL PARAMETER -> GetExercice(int id = {id}):  exercice = {exercice}");

                return NotFound();
            }

            return exercice;
        }

        // PUT: api/Exercices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExercice(int id, Exercice exercice)
        {
            var log = Log.ForContext<StimulusAPI.Controllers.ExercicesController>();

            if (id != exercice.Id)
            {
                log.Information($"INVALID ID -> PutExercice(int id = {id}, Exercice exercice = {exercice}):  PUT REQUEST L'id fourni ne correspond pas à l'exercice : {id} != {exercice}");

                return BadRequest();
            }

            _context.Entry(exercice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciceExists(id))
                {
                    log.Information($"INVALID ID -> PutExercice(int id = {id}, Exercice exercice = {exercice}):  PUT REQUEST L'id fourni ne à aucun exercice");

                    return NotFound();
                }
                else
                {
                    log.Information($"ERROR -> PutExercice(int id = {id}, Exercice exercice = {exercice}):  PUT REQUEST THROWING ERROR");

                    throw;
                }
            }
            log.Information($"NO CONTENT -> PutExercice(int id = {id}, Exercice exercice = {exercice}):  PUT REQUEST Aucun contenu, aucun changement possible");

            return NoContent();
        }

        // POST: api/Exercices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Exercice>> PostExercice(Exercice exercice)
        {
            var log = Log.ForContext<StimulusAPI.Controllers.ExercicesController>();

            _context.Exercices.Add(exercice);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ExerciceExists(exercice.Id))
                {
                    log.Information($"CONFLICT -> PostExercice(Exercice exercice = {exercice}):  POST REQUEST Un exercice dont l'id = {exercice.Id} existe déjà");

                    return Conflict();
                }
                else
                {
                    log.Information($"ERROR -> PostExercice(Exercice exercice = {exercice}):  POST REQUEST THROWING ERROR");

                    throw;
                }
            }

            return CreatedAtAction("GetExercice", new { id = exercice.Id }, exercice);
        }

        // DELETE: api/Exercices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercice(int id)
        {
            var log = Log.ForContext<StimulusAPI.Controllers.ExercicesController>();

            var exercice = await _context.Exercices.FindAsync(id);
            if (exercice == null)
            {
                log.Information($"NULL PARAMETER -> DeleteExercice(int id = {id}):  DELETE REQUEST L'exercice est null, il ne peut pas être supprimé");

                return NotFound();
            }

            _context.Exercices.Remove(exercice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExerciceExists(int id)
        {
            return _context.Exercices.Any(e => e.Id == id);
        }
    }
}
