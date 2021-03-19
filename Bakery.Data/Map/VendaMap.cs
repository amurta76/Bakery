using Bakery.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Data.Map
{
    public class VendaMap : IEntityTypeConfiguration<Venda>
    {
        public void Configure(EntityTypeBuilder<Venda> builder)
        {
            builder.ToTable("Venda");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
                        
            builder.Property(x => x.IdCaixa).IsRequired();

            builder.Property(x => x.Data).IsRequired();

            builder.Property(x => x.TipoPagamento).IsRequired();

            builder.Property(x => x.Valor).HasColumnType("decimal(18,2)").IsRequired();

            builder.Property(x => x.ValorRecebido).HasColumnType("decimal(18,2)").IsRequired();


            builder.HasOne(u => u.Caixa)
                .WithMany(c => c.Vendas)
                .HasForeignKey(e => e.IdCaixa);



        }
    }
}
