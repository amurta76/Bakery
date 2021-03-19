using Bakery.Dominio.Enum;
using Bakery.Dominio.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Dominio
{
    public class CaixaDescarte : IEntity
    {
        public int Id { get; set; }
        public int IdCaixa { get; set; }
        public Caixa Caixa { get; set; }
        public int IdProdutoFinal { get; set; }
        public ProdutoFinal ProdutoFinal { get; set; }
        public decimal Quantidade { get; set; }
    }
}
