using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Univers.Common.Models;

namespace Univers.DAL.EFCore.Configs
{
    internal class GalaxyConfig : IEntityTypeConfiguration<Galaxy>
    {
        public void Configure(EntityTypeBuilder<Galaxy> builder)
        {
            // Table
            builder.ToTable("Galaxy", t =>
            {
                t.HasCheckConstraint("CK_Galaxy__Name", "LEN([Name]) > 0");
            });

            // Clef primaire
            builder.HasKey(g => g.Id)
                .HasName("PK_Galaxy")
                .IsClustered();

            // Index
            builder.HasIndex(g => g.Name)
                .HasDatabaseName("UK_Galaxy__Name")
                .IsUnique();

            // Props
            builder.Property(g => g.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            builder.Property(g => g.Name)
                .HasColumnName("Name")
                //.HasColumnType("NVARCHAR(50)")
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

            builder.Property(g => g.Description)
                .HasColumnName("Description")
                .HasMaxLength(5_000)
                .IsUnicode();
        }
    }
}
