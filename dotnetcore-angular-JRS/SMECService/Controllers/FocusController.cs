using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SMECService.Models;
using SMECService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SMECService.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAllOrigins")]
    public class FocusController : Controller
    {
        private readonly CEMSContext _context;

        public FocusController(CEMSContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Focus> GetAll()
        {
            return _context.Focus
                .Include(f => f.Analyzers)
                    .ThenInclude( a => a.Sensors )
                        .ThenInclude( s => s.MeasuringComponent)
                .Include(f => f.Analyzers)
                    .ThenInclude(a => a.Sensors)
                         .ThenInclude(s => s.Unit)
                .ToList();

        }

        [HttpGet("{id}", Name = "GetFocus")]
        public IActionResult GetById(long id)
        {
            var item = _context.Focus
                .Include(f => f.Analyzers)
                    .ThenInclude(a => a.Sensors)
                        .ThenInclude(s => s.MeasuringComponent)
                .Include(f => f.Analyzers)
                    .ThenInclude(a => a.Sensors)
                         .ThenInclude(s => s.Unit)
                .FirstOrDefault(t => t.FocusId == id);
            if( item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpGet("{id}/CurrentStatus", Name = "GetFocusCurrentstatus")]
        public IActionResult GetCurrentStatus(long id)
        {
            var item = _context.Focus
                .FirstOrDefault(t => t.FocusId == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(new { StatusCode = 1 });
        }

        [HttpPost]
        public IActionResult Create([FromBody] Focus item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.Focus.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetFocus", new { id = item.FocusId }, item);
        }        
    }
}
