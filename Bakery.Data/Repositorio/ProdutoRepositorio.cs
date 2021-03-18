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

        public List<ProdutoListagemDTO> ListarMateriasPrima(string nome, bool mostrarInativos)
        {
            List<Produto> listamateriaprima = new List<Produto>(0);

            listamateriaprima = _contexto.Set<Produto>().Where(u => u.TipoProduto == EnumTipoProduto.MATERIA_PRIMA && (u.Nome == nome || string.IsNullOrEmpty(nome))
                                                                        && (u.Situacao == !mostrarInativos || mostrarInativos)).ToList();

            return listamateriaprima.Select(s =>

            new ProdutoListagemDTO()
            {
                Nome = s.Nome,
                Quantidade = s.QuantidadeEstoque,
                UnidadedeMedida = s.UnidadeMedida,
                Id = s.Id

            }).OrderBy(x => x.Nome).ToList();

        }
        public List<ProdutoFinalListagemDTO> ListarProdutosFinal(string nome, bool mostrarInativos)
        {
            List<Produto> listaprodutofinal = new List<Produto>(0);
            List<EnumTipoProduto> tiposProduto = new List<EnumTipoProduto>(0);
            tiposProduto.Add(EnumTipoProduto.PRODUZIDO);
            tiposProduto.Add(EnumTipoProduto.TERCERIZADO);

            listaprodutofinal = _contexto.Set<Produto>().Where(u => tiposProduto.Contains(u.TipoProduto)
                                                                    && (u.Nome == nome || string.IsNullOrEmpty(nome))
                                                                    && (u.Situacao == !mostrarInativos || mostrarInativos)).ToList();

            return (List<ProdutoFinalListagemDTO>)listaprodutofinal.Select(s =>

            new ProdutoFinalListagemDTO()
            {
                Nome = s.Nome,
                Tipo = s.TipoProduto.ToString(),
                Quantidade = s.QuantidadeEstoque,
                UnidadedeMedida = s.UnidadeMedida,
                Id = s.Id

            }).OrderBy(x => x.Nome).ToList();

        }


        public ProdutoFinalProduzido SelecionarProdutoFinalProduzido(int id)
        {
            return _contexto.Set<ProdutoFinalProduzido>().Include(p => p.Receita).FirstOrDefault(x => x.Id == id);
        }

    }
}



