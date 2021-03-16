using Bakery.Data.Interface;
using Bakery.Data.Repository;
using Bakery.Dominio;
using Bakery.Dominio.Dto;
using Bakery.Dominio.Enum;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Bakery.Data.Repositorio
{
    public class ProdutoRepositorio : BaseRepositorio<Produto>, IProdutoRepositorio
    {
        public ProdutoRepositorio(Contexto contexto) : base(contexto)
        { }
        public decimal BuscarQuantidadeEstoque(int id)
        {
            var produto = Selecionar(id);
            _contexto.Entry(produto).State = EntityState.Detached;
            return produto.QuantidadeEstoque;
        }

        public List<ProdutoListagemDTO> ListarMateriaPrima(string nome, bool mostrarInativos,EnumTipoProduto tipoProduto )
        {
            List<Produto> listamateriaprima = new List<Produto>(0);
           
                listamateriaprima = _contexto.Set<Produto>().Where(u => u.TipoProduto == tipoProduto && (u.Nome == nome || string.IsNullOrEmpty(nome))
                                                                            && (u.Situacao == !mostrarInativos|| mostrarInativos)).ToList();
           
            return listamateriaprima.Select(s =>

            new ProdutoListagemDTO()
            {
                Nome = s.Nome,
                Quantidade = s.QuantidadeEstoque,
                UnidadedeMedida = s.UnidadeMedida,
                Id = s.Id

            }).OrderBy(x => x.Nome).ToList();          

        }

    }


}
