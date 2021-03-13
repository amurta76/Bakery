using Bakery.Dominio.Enum;
using Bakery.Dominio.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Dominio
{
    public class Produto: IEntity
    {
        public int Id { get; set; }
        public EnumTipoProduto TipoProduto { get; set; }
        public string Nome { get; set; }
        public string UnidadeMedida { get; set; }
        public decimal QuantidadeEstoque { get; set; }
        public bool Situacao { get; set; }
    }
}
