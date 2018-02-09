using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace SMECService.Data
{
    public class CEMSContextFactory: IDesignTimeDbContextFactory<CEMSContext>
    {
        public CEMSContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CEMSContext>();
            optionsBuilder.UseSqlServer("Server=192.168.10.105;Database=SMEC;User Id=sa;Password=qabasa.100.;MultipleActiveResultSets=true");

            return new CEMSContext(optionsBuilder.Options);
        }
    }
}
