using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Dominio.Dto
{
    public class VendaDTO
    {
        public decimal Troco { get; set; }
        public string Mensagem { get; set; }
        public decimal ValorPago { get; set; }
    }
}
