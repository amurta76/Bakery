using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Dominio
{
    public class ProdutoFinalProduzido : ProdutoFinal
    {
        public List<Ingrediente> Receita { get; set; }

    }
}
