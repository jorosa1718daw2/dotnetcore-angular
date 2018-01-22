using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SMECService.Models;
using SMECService.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SMECService.Controllers
{
    [Route("api/[controller]")]
    public class UnitController : Controller
    {
        private readonly CEMSContext _context;

        public UnitController(CEMSContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Unit> GetAll()
        {
            return _context.Units.ToList();
        }

        [HttpGet("{id}", Name = "GetUnit")]
        public IActionResult GetById(long id)
        {
            var item = _context.Units.FirstOrDefault(t => t.UnitId == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Unit item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.Units.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetUnit", new { id = item.UnitId }, item);
        }




    }
}
