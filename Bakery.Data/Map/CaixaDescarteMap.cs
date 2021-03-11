using Bakery.Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Data.Map
{
    public class CaixaDescarteMap : IEntityTypeConfiguration<CaixaDescarte>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CaixaDescarte> builder)
        {
            builder.ToTable("CaixaDescarte");

            builder.HasKey(X => X.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Quantidade).HasColumnType("decimal(18,3)").IsRequired();

            builder.HasOne(u => u.Caixa)
                .WithMany(c => c.Descartes)
                .HasForeignKey(e => e.IdCaixa);

            builder.HasOne(u => u.ProdutoFinal)
                .WithMany(c => c.Descartes)
                .HasForeignKey(e => e.IdProdutoFinal);

        }
    }
}
