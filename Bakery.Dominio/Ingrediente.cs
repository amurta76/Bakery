using Bakery.Dominio.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Dominio
{
    public class Ingrediente : IEntity
    {
        public int Id { get; set; }

        public int IdMateriaPrima { get; set; }
        public ProdutoMateriaPrima MateriaPrima { get; set; }
        public decimal Quantidade { get; set; }
    }
}
