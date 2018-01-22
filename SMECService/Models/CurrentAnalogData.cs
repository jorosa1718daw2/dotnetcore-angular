using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SMECService.Models
{
    [Table("analog_raw_rt")]
    public class CurrentAnalogData
    {

        [Column("id")]
        public int Id { get; set; }
        [Column("updated_at")]
        public DateTime TimeStamp { get; set; }
        [Column("avg_value")]
        public double Value { get; set; }
        public int Samples { get; set; }
    }
}
