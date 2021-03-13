using Bakery.Data.Interface;
using Bakery.Data.Repository;
using Bakery.Dominio;

namespace Bakery.Data.Repositorio
{
    public class ProdutoRepositorio : BaseRepositorio<Produto>, IProdutoRepositorio
    {
        public ProdutoRepositorio(Contexto contexto) : base(contexto)
        {
        }
    }
}
