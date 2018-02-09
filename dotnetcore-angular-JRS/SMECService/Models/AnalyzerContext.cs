using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SMECService.Models
{
    public class AnalyzerContext : DbContext
    {
        public AnalyzerContext(DbContextOptions<AnalyzerContext> options)
            : base(options)
        {
        }

        public DbSet<Analyzer> Analyzers { get; set; }
    }
}
