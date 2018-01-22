using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SMECService.Models
{
    [Table("MeasuringComponent")]
    public class MeasuringComponent
    {
        public int MeasuringComponentId { get; set; }
        public string Name { get; set; }
    }
}
