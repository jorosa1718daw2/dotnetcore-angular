using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SMECService.Models
{
    public class FocusContext : DbContext
    {
        public FocusContext(DbContextOptions<FocusContext> options)
            : base(options)
        {
        }

        public DbSet<Focus> Focus { get; set; }
    }
}
