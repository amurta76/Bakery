using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Dominio
{
    public class ProdutoFinal : Produto
    {
        public decimal Valor { get; set; }

        public List<VendaItem> VendaItems { get; set; }
        public List<CaixaDescarte> Descartes { get; set; }
    }
}
