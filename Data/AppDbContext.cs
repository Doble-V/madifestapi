using madi_fest_api.Models;
using Microsoft.EntityFrameworkCore;

namespace madi_fest_api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Fest> Fests => Set<Fest>();
        public DbSet<Invitado> Invitados => Set<Invitado>();
        public DbSet<Acompanante> Acompanantes => Set<Acompanante>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la relación Invitado -> Fest
            modelBuilder.Entity<Invitado>(entity =>
            {
                entity.ToTable("MF_GUESTS"); // Nombre de tu tabla física

                // Definimos que FestId es la llave foránea que apunta a la clase Fest
                entity.HasOne(i => i.Fest)
                      .WithMany(f => f.Guests)
                      .HasForeignKey(i => i.FestId);
            });

            // Configuración de la relación Acompanante -> Invitado
            modelBuilder.Entity<Acompanante>(entity =>
            {
                entity.ToTable("MF_COMPANIONS"); // Asegúrate que este sea el nombre de tu tabla de acompañantes

                entity.HasOne(a => a.Invitado)
                      .WithMany(i => i.Acompanantes)
                      .HasForeignKey(a => a.InvitadoId);
            });
        }
    }
}