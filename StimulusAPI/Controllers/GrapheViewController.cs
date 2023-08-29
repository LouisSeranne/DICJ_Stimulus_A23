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
    public class GrapheViewController : ControllerBase
    {
        private readonly DevProjetStimulusContext _context;

        public GrapheViewController(DevProjetStimulusContext context)
        {
            _context = context;
        }

        // GET: api/Graphes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GrapheView>>> GetGraphesView()
        {
            return await _context.GrapheViews.ToListAsync();

        }

        // GET: api/Graphes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<GrapheView>>> GetGrapheView(int id)
        {
            var graphe = (IEnumerable<GrapheView>)_context.GrapheViews.Where(g => g.GrapheId == id);

            if (graphe == null)
            {
                return NotFound();
            }


            return new ActionResult<IEnumerable<GrapheView>>(graphe);
        }
    }
}
