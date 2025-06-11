using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Univers.Common.Models;

namespace Univers.DAL.EFCore.Configs
{
    internal class PlanetConfig : IEntityTypeConfiguration<Planet>
    {
        public void Configure(EntityTypeBuilder<Planet> builder)
        {
            // Table
            builder.ToTable("Planet");

            // Clef
            builder.HasKey(p => p.Id)
                .HasName("PK_Planet");

            // Props
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

            builder.Property(p => p.Gravity)
                .HasPrecision(7, 4)
                .HasConversion(
                    vCode => Convert.ToDecimal(vCode),  // To DB
                    vDb => Convert.ToDouble(vDb)        // From DB
                );
                // Le champs de la DB est « DECIMAL(7,4) » et le code en « double », il faut donc faire une conversion entre les 2 types.
                // Alternative => .HasConversion<decimal>();
        }
    }
}
