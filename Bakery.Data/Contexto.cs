using Bakery.Data.Map;
using Bakery.Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Data
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions options) : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Caixa> Caixa { get; set; }
        public DbSet<CaixaDescarte> CaixaDescarte { get; set; }
        public DbSet<Ingrediente> Ingrediente { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<ProdutoFinal> ProdutoFinal { get; set; }
        public DbSet<ProdutoFinalProduzido> ProdutoFinalProduzido { get; set; }
        public DbSet<ProdutoMateriaPrima> ProdutoMateriaPrima { get; set; }
        public DbSet<Venda> Venda { get; set; }
        public DbSet<VendaItem> VendaItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new CaixaMap());
            modelBuilder.ApplyConfiguration(new CaixaDescarteMap());            
            modelBuilder.ApplyConfiguration(new IngredienteMap());
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new ProdutoFinalMap());
            modelBuilder.ApplyConfiguration(new ProdutoFinalProduzidoMap());
            modelBuilder.ApplyConfiguration(new ProdutoMateriaPrimaMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new VendaMap());
            modelBuilder.ApplyConfiguration(new VendaItemMap());
        }


    }
}
