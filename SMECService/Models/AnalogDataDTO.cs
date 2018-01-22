using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMECService.Models
{
    public class AnalogDataDTO
    {
        public DateTime TimeStamp { get; set; }
        public double Value { get; set; }
        public int StatusCode { get; set; }
        public int Samples { get; set; }

    }
}
