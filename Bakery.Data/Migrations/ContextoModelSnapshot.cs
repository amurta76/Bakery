﻿// <auto-generated />
using System;
using Bakery.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bakery.Data.Migrations
{
    [DbContext(typeof(Contexto))]
    partial class ContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Bakery.Dominio.Caixa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataAbertura")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataFechameto")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<int>("SituacaoCaixa")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Caixa");
                });

            modelBuilder.Entity("Bakery.Dominio.CaixaDescarte", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdCaixa")
                        .HasColumnType("int");

                    b.Property<int>("IdProdutoFinal")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantidade")
                        .HasColumnType("decimal(18,3)");

                    b.HasKey("Id");

                    b.HasIndex("IdCaixa");

                    b.HasIndex("IdProdutoFinal");

                    b.ToTable("CaixaDescarte");
                });

            modelBuilder.Entity("Bakery.Dominio.Ingrediente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdMateriaPrima")
                        .HasColumnType("int");

                    b.Property<int?>("ProdutoFinalProduzidoId")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantidade")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("IdMateriaPrima");

                    b.HasIndex("ProdutoFinalProduzidoId");

                    b.ToTable("Ingrediente");
                });

            modelBuilder.Entity("Bakery.Dominio.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("ProdutoType")
                        .HasColumnType("int");

                    b.Property<decimal>("QuantidadeEstoque")
                        .HasColumnType("decimal(18,3)");

                    b.Property<bool>("Situacao")
                        .HasColumnType("bit");

                    b.Property<int>("TipoProduto")
                        .HasColumnType("int");

                    b.Property<string>("UnidadeMedida")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Produto");

                    b.HasDiscriminator<int>("ProdutoType").HasValue(1);
                });

            modelBuilder.Entity("Bakery.Dominio.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<int>("PerfilUsuario")
                        .HasColumnType("int");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Bakery.Dominio.Venda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdCaixa")
                        .HasColumnType("int");

                    b.Property<int>("TipoPagamento")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("IdCaixa");

                    b.ToTable("Venda");
                });

            modelBuilder.Entity("Bakery.Dominio.VendaItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdProdutoFinal")
                        .HasColumnType("int");

                    b.Property<int>("IdVenda")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantidade")
                        .HasColumnType("decimal(18,3)");

                    b.Property<decimal>("ValorItem")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ValorUnitario")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("IdProdutoFinal");

                    b.HasIndex("IdVenda");

                    b.ToTable("VendaItem");
                });

            modelBuilder.Entity("Bakery.Dominio.ProdutoFinal", b =>
                {
                    b.HasBaseType("Bakery.Dominio.Produto");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasDiscriminator().HasValue(3);
                });

            modelBuilder.Entity("Bakery.Dominio.ProdutoMateriaPrima", b =>
                {
                    b.HasBaseType("Bakery.Dominio.Produto");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("Bakery.Dominio.ProdutoFinalProduzido", b =>
                {
                    b.HasBaseType("Bakery.Dominio.ProdutoFinal");

                    b.HasDiscriminator().HasValue(4);
                });

            modelBuilder.Entity("Bakery.Dominio.Caixa", b =>
                {
                    b.HasOne("Bakery.Dominio.Usuario", "Usuario")
                        .WithMany("Caixas")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Bakery.Dominio.CaixaDescarte", b =>
                {
                    b.HasOne("Bakery.Dominio.Caixa", "Caixa")
                        .WithMany("Descartes")
                        .HasForeignKey("IdCaixa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bakery.Dominio.ProdutoFinal", "ProdutoFinal")
                        .WithMany("Descartes")
                        .HasForeignKey("IdProdutoFinal")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Caixa");

                    b.Navigation("ProdutoFinal");
                });

            modelBuilder.Entity("Bakery.Dominio.Ingrediente", b =>
                {
                    b.HasOne("Bakery.Dominio.ProdutoMateriaPrima", "MateriaPrima")
                        .WithMany("Ingredientes")
                        .HasForeignKey("IdMateriaPrima")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bakery.Dominio.ProdutoFinalProduzido", null)
                        .WithMany("Receita")
                        .HasForeignKey("ProdutoFinalProduzidoId");

                    b.Navigation("MateriaPrima");
                });

            modelBuilder.Entity("Bakery.Dominio.Venda", b =>
                {
                    b.HasOne("Bakery.Dominio.Caixa", "Caixa")
                        .WithMany("Vendas")
                        .HasForeignKey("IdCaixa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Caixa");
                });

            modelBuilder.Entity("Bakery.Dominio.VendaItem", b =>
                {
                    b.HasOne("Bakery.Dominio.ProdutoFinal", "ProdutoFinal")
                        .WithMany("VendaItems")
                        .HasForeignKey("IdProdutoFinal")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bakery.Dominio.Venda", "Venda")
                        .WithMany("Itens")
                        .HasForeignKey("IdVenda")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProdutoFinal");

                    b.Navigation("Venda");
                });

            modelBuilder.Entity("Bakery.Dominio.Caixa", b =>
                {
                    b.Navigation("Descartes");

                    b.Navigation("Vendas");
                });

            modelBuilder.Entity("Bakery.Dominio.Usuario", b =>
                {
                    b.Navigation("Caixas");
                });

            modelBuilder.Entity("Bakery.Dominio.Venda", b =>
                {
                    b.Navigation("Itens");
                });

            modelBuilder.Entity("Bakery.Dominio.ProdutoFinal", b =>
                {
                    b.Navigation("Descartes");

                    b.Navigation("VendaItems");
                });

            modelBuilder.Entity("Bakery.Dominio.ProdutoMateriaPrima", b =>
                {
                    b.Navigation("Ingredientes");
                });

            modelBuilder.Entity("Bakery.Dominio.ProdutoFinalProduzido", b =>
                {
                    b.Navigation("Receita");
                });
#pragma warning restore 612, 618
        }
    }
}
