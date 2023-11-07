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
using StimulusAPI.ViewModels;

namespace StimulusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EtudiantsController : ControllerBase
    {
        private readonly TestStimulusProjet_Evolution _context;

        public EtudiantsController(TestStimulusProjet_Evolution context)
        {
            _context = context;
        }

        // GET: api/Etudiants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Etudiant>>> GetEtudiants()
        {
            var log = Log.ForContext<StimulusAPI.Controllers.EtudiantsController>();
            log.Information($"GetEtudiants():  Context : {_context}"); //Surveiller, risque d'avoir besoin d'un ToString()

            return await _context.Etudiants.ToListAsync();
        }

        // GET: api/Etudiants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EtudiantVM>> GetEtudiant(string id)
        {
            var log = Log.ForContext<StimulusAPI.Controllers.EtudiantsController>();

            var etudiant =  _context.Etudiants.Where(e => e.CodeDa == id).Include(e => e.Groupes).FirstOrDefault();


            if (etudiant == null)
            {
                log.Information($"NULL PARAMETER -> GetEtudiant(string id = {id}): GET REQUEST Le paramètre id est null"); //Surveiller, risque d'avoir besoin d'un ToString()

                return NotFound();
            }

            var response = new EtudiantVM(etudiant);

            return response;
        }

        // PUT: api/Etudiants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEtudiant(string id, Etudiant etudiant)
        {
            var log = Log.ForContext<StimulusAPI.Controllers.EtudiantsController>();

            if (id != etudiant.CodeDa)
            {
                log.Information($"INVALID ID -> PutEtudiant(string id = {id}, Etudiant etudiant = {etudiant}): PUT REQUEST L'id ne correspond pas au code de DA de l'étudiant"); //Surveiller, risque d'avoir besoin d'un ToString()

                return BadRequest();
            }

            _context.Entry(etudiant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EtudiantExists(id))
                {
                    log.Information($"INVALID ID -> PutEtudiant(string id = {id}, Etudiant etudiant = {etudiant}): PUT REQUEST L'id ne correspond à aucun étudiant"); //Surveiller les appels d'identifiant, appeler l'objet vs appeler son id ex: etudiant vs etudiant.CodeDa

                    return NotFound();
                }
                else
                {
                    log.Information($"INVALID ID -> PutEtudiant(string id = {id}, Etudiant etudiant = {etudiant}): PUT REQUEST THROWING ERROR"); 

                    throw;
                }
            }
            log.Information($"NO CONTENT -> PutEtudiant(string id = {id}, Etudiant etudiant = {etudiant}): PUT REQUEST  aucun contenu, aucun changement possible ");
            return NoContent();
        }

        // POST: api/Etudiants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Etudiant>> PostEtudiant(Etudiant etudiant)
        {
            var log = Log.ForContext<StimulusAPI.Controllers.EtudiantsController>();

            _context.Etudiants.Add(etudiant);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EtudiantExists(etudiant.CodeDa))
                {
                    log.Information($"CONFLICT -> PostEtudiant(Etudiant etudiant = {etudiant}): POST REQUEST  L'étudiant existe déjà et ne peut pas être ajouté ");

                    return Conflict();
                }
                else
                {
                    log.Information($"ERROR -> PostEtudiant(Etudiant etudiant = {etudiant}): POST REQUEST  ÉCHEC DE L'AJOUT THROWING ERROR ");

                    throw;
                }
            }

            return CreatedAtAction("GetEtudiant", new { id = etudiant.CodeDa }, etudiant);
        }

        // DELETE: api/Etudiants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEtudiant(string id)
        {
            var log = Log.ForContext<StimulusAPI.Controllers.EtudiantsController>();

            var etudiant = await _context.Etudiants.FindAsync(id);
            if (etudiant == null)
            {
                log.Information($"INVALID ID -> DeleteEtudiant(string id = {id}): DELETE REQUEST  L'étudiant est null : etudiant = {etudiant} ");

                return NotFound();
            }

            _context.Etudiants.Remove(etudiant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EtudiantExists(string id)
        {
            return _context.Etudiants.Any(e => e.CodeDa == id);
        }
    }
}
