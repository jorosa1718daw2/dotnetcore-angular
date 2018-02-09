using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SMECService.Models
{
    [Table("Focus")]
    public class Focus
    {
        public int FocusId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Analyzer> Analyzers { get; set; }
    }
}
