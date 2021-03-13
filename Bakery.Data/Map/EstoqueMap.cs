using Bakery.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Data.Map
{
    public class EstoqueMap : IEntityTypeConfiguration<Estoque>
    {
        public void Configure(EntityTypeBuilder<Estoque> builder)
        {
            builder.ToTable("Estoque");
            
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            
            builder.Property(x => x.IdProduto).IsRequired();
            
            builder.Property(x => x.Data).IsRequired();
            
            builder.Property(x => x.TipoEstoque).IsRequired();
            
            builder.Property(x => x.Quantidade).HasColumnType("decimal(18,3)").IsRequired();
            
            builder.HasOne(x => x.Produto)
                .WithMany(x => x.Estoques)
                .HasForeignKey(x => x.IdProduto);



        }
    }
}
