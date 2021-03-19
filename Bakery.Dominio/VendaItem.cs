using Bakery.Dominio.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Dominio
{
    public class VendaItem : IEntity
    {
        public int Id { get; set; }
        public int IdVenda { get; set; }
        public Venda Venda { get; set; }
        public int IdProdutoFinal { get; set; }
        public ProdutoFinal ProdutoFinal { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorItem { get; set; }

    }
}
