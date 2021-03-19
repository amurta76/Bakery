using Bakery.Dominio.Enum;
using Bakery.Dominio.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Dominio
{
    public class Venda : IEntity
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorRecebido { get; set; }
        public int IdCaixa { get; set; }
        public Caixa Caixa { get; set; }
        public EnumTipoPagamento TipoPagamento { get; set; }
        public List<VendaItem> Itens { get; set; }
    }
}
