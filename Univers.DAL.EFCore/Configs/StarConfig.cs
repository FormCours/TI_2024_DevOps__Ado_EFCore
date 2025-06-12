using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univers.Common.Models;

namespace Univers.DAL.EFCore.Configs
{
    internal class StarConfig : IEntityTypeConfiguration<Star>
    {
        public void Configure(EntityTypeBuilder<Star> builder)
        {
            // Table
            builder.ToTable("Star");

            // Key
            builder.HasKey(s => s.Id)
                .IsClustered()
                .HasName("PK_Star");

            // Prop
            builder.Property(s => s.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("Id");

            builder.Property(s => s.Name)
                .HasColumnName("Name")
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode();

            builder.Property(s => s.IsDeath)
                .HasColumnName("IsDeath")
                .HasColumnType("BIT")
                .HasDefaultValueSql("0");

            // Index
            builder.HasIndex(s => s.Name)
                .HasDatabaseName("IDX_Star__Name");
        }
    }
}
