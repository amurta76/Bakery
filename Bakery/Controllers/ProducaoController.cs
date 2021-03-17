using Bakery.Data.Interface;
using Bakery.Dominio;
using Bakery.Dominio.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMINISTRADOR, PADEIRO")]
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
        public IActionResult Post([FromBody] ProdutoFinalProduzido produtoParaProduzir)
        {

            //busca o Produto final da base, para pegar os dados atualizados de receita
            ProdutoFinalProduzido produtoFinalProduzido = _produtoRepositorio.SelecionarProdutoFinalProduzido(produtoParaProduzir.Id);

            //QuantidadeEstoque = qtd para Produzir
            // para verificacao
            foreach (var item in produtoFinalProduzido.Receita)
            {
                //verifica estoque materia prima
                ProdutoMateriaPrima materiaPrima = (ProdutoMateriaPrima) _produtoRepositorio.Selecionar(item.IdMateriaPrima);
                if (!materiaPrima.VerificaEstoqueQuantidadeMateiraPrima(item.Quantidade * produtoParaProduzir.QuantidadeEstoque))
                {
                    return BadRequest($"Não possui estoque suficiente para o produto {item.MateriaPrima.Nome}.");
                }
            }

            //cria a movimentacao de estoque do produto final
            Estoque estoque = new Estoque()
            {
                Produto = produtoFinalProduzido,
                Data = new DateTime(),
                Quantidade = produtoParaProduzir.QuantidadeEstoque,
                TipoEstoque = EnumTipoEstoque.ENTRADA
            };
            _estoqueRepositorio.Incluir(estoque);
            produtoFinalProduzido.QuantidadeEstoque += estoque.Quantidade;
            

            // para retirada do estoque
            foreach (var item in produtoFinalProduzido.Receita)
            {
                ProdutoMateriaPrima materiaPrima = (ProdutoMateriaPrima)_produtoRepositorio.Selecionar(item.IdMateriaPrima);
                //cria a movimentacao de estoque do produto materia prima
                Estoque estoqueMateiraPrima = new Estoque()
                {
                    Produto = materiaPrima,
                    Data = new DateTime(),
                    Quantidade = produtoParaProduzir.QuantidadeEstoque * item.Quantidade,
                    TipoEstoque = EnumTipoEstoque.SAIDA
                };
                _estoqueRepositorio.Incluir(estoqueMateiraPrima);
                materiaPrima.QuantidadeEstoque -= estoqueMateiraPrima.Quantidade;
                _produtoRepositorio.Alterar(materiaPrima);
            }

            _produtoRepositorio.Alterar(produtoFinalProduzido);
            return Ok("O produto final produzido selecionado foi atualizado com sucesso no estoque.");

        }

    }
}
