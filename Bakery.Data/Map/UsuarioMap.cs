using Bakery.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Data.Map
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Nome).HasColumnType("varchar(150)").IsRequired();

            builder.Property(x => x.Email).HasColumnType("varchar(100)").IsRequired();

            builder.Property(x => x.DataNascimento).IsRequired();

            builder.Property(x => x.PerfilUsuario).IsRequired();

            builder.Property(x => x.Senha).HasColumnType("varchar(15)").IsRequired();            

        }
    }
}
