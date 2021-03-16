using Bakery.Data.Interface;
using Bakery.Data.Repository;
using Bakery.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Data.Repositorio
{
    public class CaixaRepositorio : BaseRepositorio <Caixa>, ICaixaRepositorio

    {
        public CaixaRepositorio(Contexto contexto) : base(contexto) { }

        public bool VerificaExistenciaDeCaixaEmAberto()
        {
            throw new NotImplementedException();
        }
    }
}
