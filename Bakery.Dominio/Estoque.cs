using Bakery.Dominio.Enum;
using Bakery.Dominio.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Dominio
{
    public class Estoque : IEntity
    {
        public int Id { get; set; }
        public int IdProduto { get; set; }
        public Produto Produto { get; set; }
        public DateTime Data { get; set; }
        public decimal Quantidade { get; set; }
        public EnumTipoEstoque TipoEstoque { get; set; }

        public bool ValidaQuantidade()
        {
            return Quantidade > decimal.Zero;
        }
    }
}
