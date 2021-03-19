using Bakery.Data.Interface;
using Bakery.Dominio;
using Bakery.Dominio.Dto;
using Bakery.Dominio.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class VendaController : Controller
    {
        private readonly IVendaRepositorio _vendaRepositorio;
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IEstoqueRepositorio _estoqueRepositorio;
        private readonly ICaixaRepositorio _caixaRepositorio;

        public VendaController(IVendaRepositorio vendaRepositorio, IProdutoRepositorio produtoRepositorio,
                                IEstoqueRepositorio estoqueRepositorio, ICaixaRepositorio caixaRepositorio)
        {
            _vendaRepositorio = vendaRepositorio;
            _produtoRepositorio = produtoRepositorio;
            _estoqueRepositorio = estoqueRepositorio;
            _caixaRepositorio = caixaRepositorio;

        }

        [HttpPost()]
        [Route("RealizarVenda")]
        [Authorize(Roles = "ADMINISTRADOR, VENDEDOR")]
        public ActionResult<VendaDTO> RealizarVenda([FromBody] Venda venda)
        {
            try
            {
                if (venda == null)
                {
                    return BadRequest("Venda não foi finalizada com sucesso");
                }

                if (venda.Data.Date < DateTime.Now.Date)
                    return BadRequest("A venda não foi realizada, a data da venda não pode ser anterior a hoje");

                if (venda.Data.Date > DateTime.Now.Date)
                    return BadRequest("A venda não foi realizada, a data da venda não pode ser posterior a hoje");

                if (venda.Valor <= 0)
                    return BadRequest("Não será permitido realizar a venda se o valor for zero ");

                if (venda.TipoPagamento == EnumTipoPagamento.DINHEIRO &&
                     venda.ValorRecebido < venda.Valor)
                    return BadRequest("Não será permitido realizar a venda. O valor recebido é menor que o valor da venda.");

                decimal totalVenda = 0;
                foreach (var item in venda.Itens)
                {
                    ProdutoFinal produtoFinal = (ProdutoFinal)_produtoRepositorio.Selecionar(item.IdProdutoFinal);

                    totalVenda += produtoFinal.Valor * item.Quantidade;

                    if ((produtoFinal.QuantidadeEstoque - item.Quantidade) < 0)
                        return BadRequest($"Não será permitido realizar a venda. O produto {produtoFinal.Nome} está com estoque indisponível");
                }


                if (venda.Valor != totalVenda)
                    return BadRequest("Não será permitido realizar a venda. O Valor da venda está incorreto.");

                var caixa = _caixaRepositorio.Selecionar(venda.Caixa.Id);

                if (!caixa.EstaAberto())
                    return BadRequest("Não será permitido realizar a venda se o caixa estiver fechado");

                venda.Caixa = caixa;

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
                vendaRetorno.Mensagem = "Venda efetuada com sucesso";


                vendaRetorno.ValorPago = venda.Valor;

                if ( venda.TipoPagamento == EnumTipoPagamento.DINHEIRO)
                    vendaRetorno.Troco = venda.ValorRecebido  - venda.Valor;

                return Ok(vendaRetorno);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}


