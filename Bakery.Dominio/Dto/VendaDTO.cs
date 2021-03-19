using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Dominio.Dto
{
    public class VendaDTO
    {
        public decimal troco { get; set; }
        public string mensagem { get; set; }

        public decimal valorPago { get; set; }
    }
}
