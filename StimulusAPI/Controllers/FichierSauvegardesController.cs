using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StimulusAPI.Context;
using StimulusAPI.Models;
using StimulusAPI.ViewModels;

namespace StimulusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FichierSauvegardesController : ControllerBase
    {
        private readonly TestStimulusProjet_Evolution _context;

        public FichierSauvegardesController(TestStimulusProjet_Evolution context)
        {
            _context = context;
        }

        [HttpGet("{idPage}/{idExercice}")]
        public async Task<List<FichierSauvegarderVM>> GetFichierSauvegarde(int idPage, int idExercice, string daEtudiant)
        {
            List<FichierSauvegarde> fichierSauvegarde = await _context.FichierSauvegardes.Where(f => f.ProgressionPageId == idPage && f.ExerciceId == idExercice && f.FichierEtudiantDa == daEtudiant).ToListAsync();

            List<FichierSauvegarderVM> fichiersReturn = new List<FichierSauvegarderVM>();

            if (fichierSauvegarde == null)
            {
                return null;
            }

            foreach (FichierSauvegarde fichier in fichierSauvegarde)
            {
                fichiersReturn.Add(new FichierSauvegarderVM(fichier));
            }

            return fichiersReturn;
        }

        // PUT: api/FichierSauvegardes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFichierSauvegarde(int id, FichierSauvegarde fichierSauvegarde)
        {
            if (id != fichierSauvegarde.Id)
            {
                return BadRequest();
            }

            _context.Entry(fichierSauvegarde).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FichierSauvegardeExists(id))
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

        [HttpPost]
        public async Task<IActionResult> SauvegarderFichiersExerciceEtudiant(List<FichierSauvegarde> fichiersSauvegarde)
        {
            List<FichierSauvegarde> nouveauFichiers = new List<FichierSauvegarde>();
            List<FichierSauvegarde> updateFichiers = new List<FichierSauvegarde>();

            foreach (FichierSauvegarde fichier in fichiersSauvegarde)
            {
                if (fichier.Id < 0)
                    nouveauFichiers.Add(fichier);
                else
                    updateFichiers.Add(fichier);
            }

            foreach (FichierSauvegarde fichier in nouveauFichiers)
            {
                fichier.Id = 0;
                await _context.FichierSauvegardes.AddAsync(fichier);
            }

            foreach (FichierSauvegarde fichier in updateFichiers)
                _context.Entry(fichier).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
            
            return Ok();
        }

        // POST: api/FichierSauvegardes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<FichierSauvegarde>> PostFichierSauvegarde(FichierSauvegarde fichierSauvegarde)
        //{
        //    _context.FichierSauvegardes.Add(fichierSauvegarde);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetFichierSauvegarde", new { id = fichierSauvegarde.Id }, fichierSauvegarde);
        //}

        // DELETE: api/FichierSauvegardes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFichierSauvegarde(int id)
        {
            var fichierSauvegarde = await _context.FichierSauvegardes.FindAsync(id);
            if (fichierSauvegarde == null)
            {
                return NotFound();
            }

            _context.FichierSauvegardes.Remove(fichierSauvegarde);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FichierSauvegardeExists(int id)
        {
            return _context.FichierSauvegardes.Any(e => e.Id == id);
        }
    }
}
