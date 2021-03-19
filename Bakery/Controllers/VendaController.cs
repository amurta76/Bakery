using Bakery.Data.Interface;
using Bakery.Dominio;
using Bakery.Dominio.Dto;
using Bakery.Dominio.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Controllers

{
    public class VendaController : Controller
    {
        private readonly IVendaRepositorio _vendaRepositorio;
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IEstoqueRepositorio _estoqueRepositorio;

        public VendaController(IVendaRepositorio vendaRepositorio, IProdutoRepositorio produtoRepositorio, IEstoqueRepositorio estoqueRepositorio)
        {
            _vendaRepositorio = vendaRepositorio;
            _produtoRepositorio = produtoRepositorio;
            _estoqueRepositorio = estoqueRepositorio;

        }

        [HttpPost()]
        [Route("RealizarVenda")]
        //[Authorize(Roles = "ADMINISTRADOR, VENDEDOR")]
        public ActionResult<VendaDTO> RealizarVenda([FromBody] Venda venda)
        {
            try
            {
                if (venda == null)
                {
                    return BadRequest("Venda não foi finalizada com sucesso");
                }

                if (venda.Data < DateTime.Now)
                    return BadRequest(" A venda não foi realizada");

                if (venda.Valor <= 1)
                {
                    return BadRequest(" Não será permitido realizar a venda se o valor for zero ");
                }

                foreach (var item in venda.Itens)
                {
                    if (item.ProdutoFinal.QuantidadeEstoque <= 0)
                        return BadRequest(" Não será permitido realizar a venda se o produtofinal estiver zeardo no estoque ");
                }

                if (!venda.Caixa.EstaAberto())
                    return BadRequest(" Não será permitido realizar a venda se o caixa estiver aberto");

                foreach (var item in venda.Itens)
                {
                    ProdutoFinal produtoFinal = (ProdutoFinal)_produtoRepositorio.Selecionar(item.IdProdutoFinal);

                    Estoque estoque = new Estoque()
                    {
                        Produto = produtoFinal,
                        Data = new DateTime(),
                        Quantidade = item.Quantidade,
                        TipoEstoque = EnumTipoEstoque.SAIDA
                    };
                    _estoqueRepositorio.Incluir(estoque);
                    produtoFinal.QuantidadeEstoque -= estoque.Quantidade;
                    _produtoRepositorio.Alterar(produtoFinal);
                }

                _vendaRepositorio.Incluir(venda);
                VendaDTO vendaRetorno = new VendaDTO();
                vendaRetorno.mensagem = "Venda efetuada com sucesso";
                vendaRetorno.valorPago = venda.Valor;
                return Ok(vendaRetorno);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}


