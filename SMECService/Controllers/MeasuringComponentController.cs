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
    public class MeasuringComponentController : Controller
    {
        private readonly CEMSContext _context;

        public MeasuringComponentController(CEMSContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<MeasuringComponent> GetAll()
        {
            return _context.MeasuringComponents.ToList();
        }

        [HttpGet("{id}", Name = "GetMeasuringComponent")]
        public IActionResult GetById(long id)
        {
            var item = _context.MeasuringComponents.FirstOrDefault(t => t.MeasuringComponentId == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] MeasuringComponent item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.MeasuringComponents.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetMeasuringComponent", new { id = item.MeasuringComponentId }, item);
        }
    }
}
