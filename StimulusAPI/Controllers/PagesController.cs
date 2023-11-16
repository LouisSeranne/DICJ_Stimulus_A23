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
    public class PagesController : ControllerBase
    {
        private readonly TestStimulusProjet_Evolution _context;

        public PagesController(TestStimulusProjet_Evolution context)
        {
            _context = context;
        }

        // GET: api/Pages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Page>>> GetPages()
        {
            return await _context.Pages.ToListAsync();
        }

        // GET: api/Pages/5
        [HttpGet("{id}")]
        public async Task<List<PageVM>> GetPageNoeud(int id)
        {
            var pageNoeud = await _context.Pages.Where(x => x.NoeudId == id).OrderBy(x=>x.Ordre).ToListAsync();

            List<PageVM> pageVM = new List<PageVM>();

            foreach (var page in pageNoeud)
            {
                PageVM pageVMadd = new PageVM(page);
                pageVM.Add(pageVMadd);
            }

            return pageVM;
        }

        // PUT: api/Pages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPage(int id, Page page)
        {
            var log = Log.ForContext<StimulusAPI.Controllers.PageComposantsController>();


            if (id != page.Id)
            {
                log.Warning($"INVALID ID -> PutPage(int id = {id}, Page page = {page}): PUT REQUEST L'id fourni ne correspond pas à l'id de page: {id} != {page.Id}");

                return BadRequest();
            }

            _context.Entry(page).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PageExists(id))
                {
                    log.Warning($"INVALID ID -> PutPage(int id = {id}, Page page = {page}): PUT REQUEST L'id fourni ne correspond à aucune page");

                    return NotFound();
                }
                else
                {
                    log.Error($"ERROR -> PutPage(int id = {id}, Page page = {page}): PUT REQUEST THROWING ERROR");

                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Pages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Page>> PostPage(Page page)
        {
            _context.Pages.Add(page);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPage", new { id = page.Id }, page);
        }

        // DELETE: api/Pages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePage(int id)
        {
            var log = Log.ForContext<StimulusAPI.Controllers.PageComposantsController>();

            var page = await _context.Pages.FindAsync(id);
            if (page == null)
            {
                log.Warning($"NULL PARAMETER -> DeletePage(int id = {id}): DELETE REQUEST La page est null");

                return NotFound();
            }

            _context.Pages.Remove(page);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PageExists(int id)
        {
            return _context.Pages.Any(e => e.Id == id);
        }
    }
}
