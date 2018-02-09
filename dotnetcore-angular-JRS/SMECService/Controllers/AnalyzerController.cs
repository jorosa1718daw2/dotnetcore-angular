using System;   
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SMECService.Models;
using SMECService.Data;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SMECService.Controllers
{
    [Route("api/[controller]")]
    public class AnalyzerController : Controller
    {
        private readonly CEMSContext _context;

        public AnalyzerController(CEMSContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Analyzer> GetAll()
        {
            return _context.Analyzers
                .Include(a => a.Focus)
                .Include(a => a.Sensors)
                    .ThenInclude(s => s.MeasuringComponent)
                .Include(a => a.Sensors)
                     .ThenInclude(s => s.Unit)
                .ToList();
        }

        [HttpGet("{id}", Name = "GetAnalyzer")]
        public IActionResult GetById(long id)
        {
            var item = _context.Analyzers
                .Include(a => a.Focus)
                .Include(a => a.Sensors)
                    .ThenInclude(s => s.MeasuringComponent)
                .Include(a => a.Sensors)
                     .ThenInclude(s => s.Unit) 
                .FirstOrDefault(t => t.AnalyzerId == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpGet("{id}/CurrentStatus", Name = "GetAnalyzerCurrentstatus")]
        public IActionResult GetCurrentStatus(long id)
        {
            var item = _context.Analyzers
                .FirstOrDefault(t => t.AnalyzerId == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(new { StatusCode = 1 });
        }

        [HttpPost]
        public IActionResult Create([FromBody] Analyzer item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.Analyzers.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetAnalyzer", new { id = item.AnalyzerId }, item);
        }
    }
}
