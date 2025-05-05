using Microsoft.EntityFrameworkCore;
using MonolitoBackend.Core.Entidade;  

namespace MonolitoBackend.Infra.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Participante> Participantes { get; set; }
        public DbSet<Evento> Eventos { get; set; }

        public DbSet<User> Users { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Evento>(entity =>
            {
                entity.Property(e => e.Data)
                    .HasColumnType("date"); // <- Mapeia para tipo SQL 'date'
            });
        }

    }
}
