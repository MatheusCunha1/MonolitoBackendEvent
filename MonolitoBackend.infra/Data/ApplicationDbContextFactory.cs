using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MonolitoBackend.Infra.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

           optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=eventcheckin;Username=postgres;Password=postgres");


            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
