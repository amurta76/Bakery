using Bakery.Data.Interface;
using Bakery.Data.Repository;
using Bakery.Dominio;
using Bakery.Dominio.Dto;
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

        public List<FechamentoCaixaDTO> VendaCaixa(int idCaixa)
        {
            var vendaCaixa = _contexto.Set<Venda>().Where(v => v.IdCaixa == idCaixa).GroupBy(v => v.TipoPagamento)
                .Select(v => new FechamentoCaixaDTO() {
                    TipoPagamento = v.Key,
                    Valor = v.Sum(s => s.Valor)
                }).ToList();

            return vendaCaixa;
        }
    }
}
