using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SMECService.Models;

namespace SMECService.Data
{
    public class CEMSContext : DbContext
    {
        public CEMSContext(DbContextOptions<CEMSContext> options) : base(options)
        {
        }

        public DbSet<Analyzer> Analyzers { get; set; }
        public DbSet<Focus> Focus { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<MeasuringComponent> MeasuringComponents { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<CurrentAnalogData> CurrentAnalogData { get; set; }
        public DbSet<HistoricalAnalogData> HistoricalAnalogData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HistoricalAnalogData>()
                .HasKey(c => new { c.Id, c.TimeStamp });
        }
    }
}
