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
            var log = Log.ForContext<StimulusAPI.Controllers.HoverController>();
            log.Information($"GetHovers(): Context : {_context}");

            return await _context.HoverViews.ToListAsync();
        }
        // GET: api/Hover/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<HoverView>>> GetHover(int id)
        {
            var log = Log.ForContext<StimulusAPI.Controllers.HoverController>();

            var hover = (IEnumerable<HoverView>)_context.HoverViews.Where(g => g.GrapheId == id);

            if (hover == null)
            {
                log.Information($"NULL PARAMETER -> GetHover(int id = {id}): GET REQUEST Le hover est null");

                return NotFound();
            }


            return new ActionResult<IEnumerable<HoverView>>(hover);
        }
    }
}
