using Bakery.Data.Repositorio;
using Bakery.Dominio;
using Bakery.Dominio.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Data.Interface
{
    public interface IVendaRepositorio : IBaseRepositorio<Venda>
    {
        List<FechamentoCaixaDTO> VendaCaixa(int idCaixa);
    }
}
