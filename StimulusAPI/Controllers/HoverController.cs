﻿using Microsoft.AspNetCore.Http;
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
        private readonly DevProjetStimulusContext _context;

        public HoverController(DevProjetStimulusContext context)
        {
            _context = context;
        }
        // GET: api/Hover
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HoverView>>> GetHovers()
        {
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


            return new ActionResult<IEnumerable<HoverView>>(hover);
        }
    }
}
