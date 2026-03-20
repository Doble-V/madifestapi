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
    }
}
