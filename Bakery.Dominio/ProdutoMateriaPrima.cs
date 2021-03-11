using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Dominio
{
    public class ProdutoMateriaPrima : Produto
    {
        public List<Ingrediente> Ingredientes { get; set; }
    }
}
