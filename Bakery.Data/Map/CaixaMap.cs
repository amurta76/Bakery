using Bakery.Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Data.Map
{
    public class CaixaMap : IEntityTypeConfiguration<Caixa>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Caixa> builder)
        {
            builder.ToTable("Caixa");

            builder.HasKey(X => X.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.DataAbertura).IsRequired();

            builder.Property(x => x.DataFechameto).IsRequired(false);

            builder.Property(x => x.IdUsuario).IsRequired();

            builder.Property(x => x.SituacaoCaixa).IsRequired();

            builder.HasOne(u => u.Usuario)
                .WithMany(c => c.Caixas)
                .HasForeignKey(e => e.IdUsuario);
        }
    }
}
