using Bakery.Data.Interface;
using Bakery.Data.Repository;
using Bakery.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bakery.Data.Repositorio
{
    public class VendaRepositorio : BaseRepositorio<Venda>, IVendaRepositorio
    {
        public VendaRepositorio(Contexto contexto) : base(contexto)
        {
        }

        public decimal VendaCaixa(int idCaixa)
        {
            var vendaCaixa = _contexto.Set<Venda>().Where(v => v.IdCaixa == idCaixa).Sum(v => v.Valor);
            return vendaCaixa;
        }
    }
}
