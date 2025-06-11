using Microsoft.EntityFrameworkCore;
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
            modelBuilder.ApplyConfiguration(new PlanetConfig());
            modelBuilder.ApplyConfiguration(new GalaxyConfig());
        }
    }
}
