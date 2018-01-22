using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SMECService.Models
{
    [Table("Unit")]
    public class Unit
    { 
        public int UnitId { get; set; }
        public string Name { get; set; }
    }
}
