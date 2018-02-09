using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SMECService.Models
{
    [Table("Sensor")]
    public class Sensor
    {
        public int SensorId { get; set; }
        public int AnalyzerId { get; set; }
        public int MeasuringComponentId { get; set; }
        public int UnitId { get; set; }

        public Analyzer Analyzer { get; set; }
        public MeasuringComponent MeasuringComponent { get; set; }
        public Unit Unit { get; set; }
        //public ICollection<HistoricalAnalogData> HistoricalAnalogData { get; set; }
        //public CurrentAnalogData CurrentAnalogData { get; set; }
    }
}
