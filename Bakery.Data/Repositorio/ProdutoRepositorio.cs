using Bakery.Data.Interface;
using Bakery.Data.Repository;
using Bakery.Dominio;
using Microsoft.EntityFrameworkCore;

namespace Bakery.Data.Repositorio
{
    public class ProdutoRepositorio : BaseRepositorio<Produto>, IProdutoRepositorio
    {
        public ProdutoRepositorio(Contexto contexto) : base(contexto)
        { }
        public decimal BuscarQuantidadeEstoque(int id)
        {
            var produto   = Selecionar(id);
            _contexto.Entry(produto).State = EntityState.Detached;
            return produto.QuantidadeEstoque;
        }

    }


}
