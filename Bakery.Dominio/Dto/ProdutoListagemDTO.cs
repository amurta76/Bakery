using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Dominio.Dto
{
   public class ProdutoListagemDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string UnidadedeMedida { get; set; }
        public Decimal Quantidade { get; set; }
        
    }
}  
