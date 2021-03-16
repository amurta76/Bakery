using Bakery.Data.Interface;
using Bakery.Dominio;
using Bakery.Dominio.Enum;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducaoController : ControllerBase
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IEstoqueRepositorio _estoqueRepositorio;
        private readonly IIngredienteRepositorio _ingredienteRepositorio;

        public ProducaoController(IProdutoRepositorio produtoRepositorio, IEstoqueRepositorio estoqueRepositorio,
                                                                        IIngredienteRepositorio ingredienteRepositorio)
        {
            _estoqueRepositorio = estoqueRepositorio;
            _produtoRepositorio = produtoRepositorio;
            _ingredienteRepositorio = ingredienteRepositorio;
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProdutoFinalProduzido produto)
        {
            //QuantidadeEstoque = qtd para Produzir

            // para verificacao
            foreach (var item in produto.Receita)
            {
                //verifica estoque materia prima
                if (!_estoqueRepositorio.VerificaEstoqueQuantidadeMateiraPrima(item.MateriaPrima,
                                                                                item.Quantidade * produto.QuantidadeEstoque))
                {
                    return BadRequest($"Não possui estoque suficiente para o produto {item.MateriaPrima.Nome}");
                }
            }

            ProdutoFinalProduzido produtoFinalProduzido = (ProdutoFinalProduzido)_produtoRepositorio.Selecionar(produto.Id);

            Estoque estoque = new Estoque()
            {
                Produto = produtoFinalProduzido,
                Data = new DateTime(),
                Quantidade = produto.QuantidadeEstoque,
                TipoEstoque = EnumTipoEstoque.ENTRADA
            };
            _estoqueRepositorio.Incluir(estoque);
            produtoFinalProduzido.QuantidadeEstoque += estoque.Quantidade;
            

            // para retirada do estoque
            foreach (var item in produto.Receita)
            {
                ProdutoMateriaPrima produtoMateriaPrima = (ProdutoMateriaPrima)_produtoRepositorio.Selecionar(produto.Id);
                Estoque estoqueMateiraPrima = new Estoque()
                {
                    Produto = produtoMateriaPrima,
                    Data = new DateTime(),
                    Quantidade = produto.QuantidadeEstoque * item.Quantidade,
                    TipoEstoque = EnumTipoEstoque.SAIDA
                };
                _estoqueRepositorio.Incluir(estoqueMateiraPrima);
                produtoMateriaPrima.QuantidadeEstoque -= estoqueMateiraPrima.Quantidade;
                _produtoRepositorio.Alterar(produtoMateriaPrima);
            }

            _produtoRepositorio.Alterar(produtoFinalProduzido);
            return Ok();

        }

    }
}
