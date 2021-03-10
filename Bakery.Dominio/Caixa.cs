using Bakery.Dominio.Enum;
using Bakery.Dominio.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Dominio
{
    public class Caixa : IEntity
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime DataFechameto { get; set; }
        public EnumSitucaoCaixa SituacaoCaixa { get; set; }
        public List<ProdutoFinal> Descarte { get; set; }

    }
}
