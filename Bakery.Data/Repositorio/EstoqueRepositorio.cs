using Bakery.Data.Interface;
using Bakery.Data.Repository;
using Bakery.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Data.Repositorio
{
    public class EstoqueRepositorio : BaseRepositorio<Estoque> ,IEstoqueRepositorio
    {
        public EstoqueRepositorio(Contexto contexto): base(contexto) 
        {
        }

        public bool VerificaEstoqueQuantidadeMateiraPrima(ProdutoMateriaPrima materiaPrima, decimal quantidade)
        {
            throw new NotImplementedException();
        }
    }
}
