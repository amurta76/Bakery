using Bakery.Dominio;
using Bakery.Dominio.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Data.Map
{
    class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto");
            
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Nome).HasColumnType("varchar(100)").IsRequired();

            builder.Property(x => x.UnidadeMedida).HasColumnType("varchar(10)").IsRequired();

            builder.Property(x => x.QuantidadeEstoque).HasColumnType("decimal(18,3)").IsRequired();

            builder.Property(x => x.Situacao).IsRequired();

            builder.Property(x => x.TipoProduto).IsRequired();

            //builder.Property(x => x.TipoProduto).IsRequired();     


            //herancas
            builder
                .HasDiscriminator<EnumTipoProduto>("TipoProduto")
                .HasValue<Produto>(EnumTipoProduto.PRODUTO)
                .HasValue<ProdutoMateriaPrima>(EnumTipoProduto.MATERIA_PRIMA)
                .HasValue<ProdutoFinalProduzido>(EnumTipoProduto.PRODUZIDO)
                .HasValue<ProdutoFinal>(EnumTipoProduto.TERCERIZADO);
                


        }
    }
}
