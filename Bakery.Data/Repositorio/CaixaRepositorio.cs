using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Data.Repositorio
{
    public class CaixaRepositorio : BaseRepositorio <Caixa>, ICaixaRepositorio

    {
        public EstoqueRepositorio(Contexto contexto) : base(contexto) { }
    }
}
