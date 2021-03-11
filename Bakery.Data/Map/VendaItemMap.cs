using Bakery.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Data.Map
{
    public class VendaItemMap : IEntityTypeConfiguration<VendaItem>
    {
        public void Configure(EntityTypeBuilder<VendaItem> builder)
        {
            builder.ToTable("VendaItem");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.IdProdutoFinal).IsRequired();

            builder.Property(x => x.Quantidade).HasColumnType("decimal(18,3)").IsRequired();

            builder.Property(x => x.ValorItem).HasColumnType("decimal(18,2)").IsRequired();

            builder.Property(x => x.ValorUnitario).HasColumnType("decimal(18,2)").IsRequired();


            builder.HasOne(u => u.Venda)
               .WithMany(c => c.Itens)
               .HasForeignKey(e => e.IdVenda);


            builder.HasOne(u => u.ProdutoFinal)
               .WithMany(c => c.VendaItems)
               .HasForeignKey(e => e.IdProdutoFinal);

        }
    }
}
