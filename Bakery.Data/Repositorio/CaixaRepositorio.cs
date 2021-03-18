using Bakery.Data.Interface;
using Bakery.Data.Repository;
using Bakery.Dominio;
using Bakery.Dominio.Enum;
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
            var contagem = _contexto.Set<Caixa>()                              
                     .Where(ca => ca.SituacaoCaixa == EnumSitucaoCaixa.ABERTO).Count();


            if (contagem == 0)
                return false;
            else
                return true;    

        }       
       
    }
}
