using definitiva.Models;
using Microsoft.EntityFrameworkCore;

namespace definitiva.Context
{
    public class DbContextPersonas : DbContext
    {
        public DbContextPersonas(DbContextOptions<DbContextPersonas> options )
        : base( options )
        {
        
        }

        public DbSet<PersonasModels> Personas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonasModels>().HasKey(p => p.id_Personas);
        }
    }
}
