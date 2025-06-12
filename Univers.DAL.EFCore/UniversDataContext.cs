using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Metadata;
using Univers.Common.Models;
using Univers.DAL.EFCore.Configs;

namespace Univers.DAL.EFCore
{
    public class UniversDataContext : DbContext
    {
        // ConnectionString
        private readonly string _connectionString;

        public UniversDataContext(string connectionString)
        {
            _connectionString = connectionString;
        }


        // DbSet (Les "modeles" accessible)
        public DbSet<Galaxy> Galaxy { get; set; }
        public DbSet<Planet> Planet { get; set; }
        public DbSet<Star> Star { get; set; }

        // Configuration
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // La config des models
            // - Sans fichier dédié
            /*
            modelBuilder.Entity<Planet>()
                .Property(p => p.Gravity)
                .HasPrecision(7, 4)
                .HasConversion<double>();
            */

            // - Ajout des fichiers de config "manuellement"
            /*
            modelBuilder.ApplyConfiguration(new PlanetConfig());
            modelBuilder.ApplyConfiguration(new GalaxyConfig());
            modelBuilder.ApplyConfiguration(new StarConfig());
            */

            // - Ajout des fichiers de config via une assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            // Relation entre les models
            // https://learn.microsoft.com/en-us/ef/core/modeling/relationships

            // - Many-to-Many : Star__Planet
            modelBuilder.Entity<Star>()
                .HasMany(s => s.Planets)   
                .WithMany(p => p.Stars)
                .UsingEntity(
                    "Rel__Star_Planet",
                    r => r.HasOne(typeof(Planet))
                        .WithMany()
                        .HasConstraintName("FK_Rel__Star_Planet__Planet")
                        .HasForeignKey("PlanetId")
                        .HasPrincipalKey("Id"),
                    l => l.HasOne(typeof(Star))
                        .WithMany()
                        .HasConstraintName("FK_Rel__Star_Planet__Star")
                        .HasForeignKey("StarId")
                        .HasPrincipalKey("Id"),
                    j => j.HasKey("StarId", "PlanetId"));

            // - One-to-Many : Galaxy__Star
            modelBuilder.Entity<Galaxy>()
                .HasMany(g => g.Stars)
                .WithOne(s => s.Galaxy)
                .HasConstraintName("FK_Star__Galaxy")
                .HasForeignKey(s => s.GalaxyId)
                .IsRequired();

        }
    }
}
