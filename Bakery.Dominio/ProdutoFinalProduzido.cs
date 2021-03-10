using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Dominio
{
    class ProdutoFinalProduzido : ProdutoFinal
    {
        public List<Ingredientes> Receita { get; set; }

    }
}
