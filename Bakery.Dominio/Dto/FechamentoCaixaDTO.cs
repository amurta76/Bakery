using Bakery.Dominio.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Dominio.Dto
{
    public class FechamentoCaixaDTO
    {
        public EnumTipoPagamento TipoPagamento { get; set; }
        public decimal Valor { get; set; }
    }
}
