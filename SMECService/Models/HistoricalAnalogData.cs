using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SMECService.Models
{
    [Table("analog_raw_ft")]
    public class HistoricalAnalogData
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("created_at")]
        public DateTime TimeStamp { get; set; }
        [Column("avg_value")]
        public double Value { get; set; }
    }
}
