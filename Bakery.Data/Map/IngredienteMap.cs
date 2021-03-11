using Bakery.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Data.Map
{
    class IngredienteMap : IEntityTypeConfiguration<Ingrediente>
    {
        public void Configure(EntityTypeBuilder<Ingrediente> builder)
        {
            builder.ToTable("Ingrediente");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.IdMateriaPrima).IsRequired();

            builder.Property(x => x.Quantidade).HasColumnType("decimal(18,2)").IsRequired();

            builder.HasOne(u => u.MateriaPrima)
             .WithMany(c => c.Ingredientes)
             .HasForeignKey(e => e.IdMateriaPrima);

        }
    }
}
