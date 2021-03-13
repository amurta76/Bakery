using Bakery.Data.Interface;
using Bakery.Data.Repository;
using Bakery.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bakery.Data.Repositorio
{
    public class IngredienteRepositorio : BaseRepositorio<Ingrediente>, IIngredienteRepositorio
    {
        public IngredienteRepositorio(Contexto contexto) : base(contexto)
        {

        }

        public bool MateriaPrimaSemProdutoFinal(int IdMateriaPrima) {

            var contagem = _contexto.Set<Ingrediente>()
                            .Where(mp => mp.IdMateriaPrima == IdMateriaPrima &&
                                    mp.ProdutoFinalProduzido.Situacao == true).Count();

            if (contagem > 0)
                return false;
            else
                return true;
                            
        }
    }
}
