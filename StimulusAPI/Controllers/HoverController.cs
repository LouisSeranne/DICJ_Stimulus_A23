using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StimulusAPI.Context;
using StimulusAPI.Models;

namespace StimulusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoverController : ControllerBase
    {
        private readonly TestStimulusProjet_Evolution _context;

        public HoverController(TestStimulusProjet_Evolution context)
        {
            _context = context;
        }
        // GET: api/Hover
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HoverView>>> GetHovers()
        {
            Console.WriteLine("Get Hover (API)");

            return await _context.HoverViews.ToListAsync();
        }
        // GET: api/Hover/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<HoverView>>> GetHover(int id)
        {
            var hover = (IEnumerable<HoverView>)_context.HoverViews.Where(g => g.GrapheId == id);

            if (hover == null)
            {
                return NotFound();
            }

            Console.WriteLine("Get Hover Id (API)");

            return new ActionResult<IEnumerable<HoverView>>(hover);
        }
    }
}
