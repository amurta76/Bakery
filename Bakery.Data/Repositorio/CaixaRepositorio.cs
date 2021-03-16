using Bakery.Data.Interface;
using Bakery.Data.Repository;
using Bakery.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bakery.Data.Repositorio
{
    public class CaixaRepositorio : BaseRepositorio <Caixa>, ICaixaRepositorio

    {
        public CaixaRepositorio(Contexto contexto) : base(contexto) { }

        public bool VerificaExistenciaDeCaixaEmAberto()
        {
            //var contagem = _contexto.Set<Caixa>()
            //                .Where(ca => ca.Id == IdCaixa &&
            //                            ca.ProdutoFinalProduzido.Situacao == true).Count();

            //if (contagem > 0)
            //    return false;
            //else
            //    return true;
            return true;
        }
    }
}
