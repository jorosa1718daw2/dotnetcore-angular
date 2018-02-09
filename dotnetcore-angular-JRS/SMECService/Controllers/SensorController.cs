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
    public class SensorController : Controller
    {
        private readonly CEMSContext _context;

        public SensorController(CEMSContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Sensor> GetAll()
        {
            return _context.Sensors
                .Include(s => s.Analyzer)
                .Include(s => s.MeasuringComponent)
                .Include(s => s.Unit)
                .ToList();

        }

        [HttpGet("{id}", Name = "GetSensor")]
        public IActionResult GetById(long id)
        {
            var item = _context.Sensors
                .Include(s => s.Analyzer)
                .Include(s => s.MeasuringComponent)
                .Include(s => s.Unit)
                .FirstOrDefault(t => t.SensorId == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        /* API QUERY SAMPLE 
         * http://localhost:63389/api/Sensor/1/CurrentAnalogData
         * 404 is returned if date format in server locale is wrong
         */
        [HttpGet("{id}/CurrentAnalogData", Name = "GetCurrentAnalogData")]
        public IActionResult GetCurrentAnalogData(long id)
        {
            var item = _context.CurrentAnalogData
                .FirstOrDefault(t => t.Id == id);

            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult( new AnalogDataDTO()
                {
                    TimeStamp = item.TimeStamp,
                    Value = item.Value,
                    Samples = item.Samples,
                    StatusCode = 1
                });
        }


        /* API QUERY SAMPLE 
         * http://localhost:63389/api/Sensor/1/HistoricalAnalogData/10-26-2017/10-27-2017
         * 404 is returned if date format in server locale is wrong
         */
        [HttpGet("{id}/HistoricalAnalogData/{start_date:datetime}/{end_date:datetime}", Name = "GetHistoricalAnalogData")]
        public IActionResult GetHistoricalAnalogData(long id, DateTime start_date, DateTime end_date)
        {
            /* QueryObjects => DTO */

            var item = _context.HistoricalAnalogData
                    .Where( a => a.Id == id && a.TimeStamp >= start_date && a.TimeStamp <= end_date)
                    .Select(p => 
                        new AnalogDataDTO()
                        {
                            TimeStamp = p.TimeStamp,
                            Value = p.Value,
                            StatusCode = 1
                        });

            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Sensor item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.Sensors.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetSensor", new { id = item.SensorId }, item);
        }
    }
}
