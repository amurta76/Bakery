using Bakery.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Data.Map
{
    public class ProdutoFinalMap : IEntityTypeConfiguration<ProdutoFinal>
    {
        public void Configure(EntityTypeBuilder<ProdutoFinal> builder)
        {
            builder.Property(x => x.Valor).HasColumnType("decimal(18,2)");
        }
    }
}
