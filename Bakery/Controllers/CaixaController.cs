using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakery.Data.Interface;
using Bakery.Data.Repositorio;
using Bakery.Dominio;
using Bakery.Dominio.Dto;
using Bakery.Dominio.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bakery.Controllers
{
    [Route("api/[controller]")]
    public class CaixaController : Controller
    {
        private readonly ICaixaRepositorio _caixaRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IEstoqueRepositorio _estoqueRepositorio;
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IVendaRepositorio _vendaRepositorio;


        public CaixaController(ICaixaRepositorio caixaRepositorio, IUsuarioRepositorio usuarioRepositorio,
                                        IEstoqueRepositorio estoqueRepositorio, IProdutoRepositorio produtoRepositorio,
                                        IVendaRepositorio vendaRepositorio)

        {
            _caixaRepositorio = caixaRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _estoqueRepositorio = estoqueRepositorio;
            _produtoRepositorio = produtoRepositorio;
            _vendaRepositorio = vendaRepositorio;
        }

        [HttpGet("{id}")]        
        [ProducesResponseType(200)] // Ok
        [ProducesResponseType(401)] //Não autorizado
        [ProducesResponseType(403)] //Proibido
        [ProducesResponseType(404)] //Não encontrado
        [ProducesResponseType(500)] //Erro interno do servidor
        public ActionResult<ProdutoFinalProduzido> Get(int id)
        {
            try
            {
                var caixa = _caixaRepositorio.Selecionar(id);

                if (caixa == null)
                {
                    return NotFound("Caixa não encontrado.");
                }

                return Ok(caixa);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("AbrirCaixa")]
        [Authorize(Roles = "ADMINISTRADOR, VENDEDOR")]
        public IActionResult Post([FromBody] Caixa caixa)
        {
            try
            {
                if (caixa == null)
                {
                    return BadRequest("Não foi possível abrir o caixa, sem dados para a abertura.");
                }

                if (_caixaRepositorio.VerificaExistenciaDeCaixaEmAberto())
                {
                    return BadRequest("Não foi possível abrir o caixa, pois já existe um caixa aberto.");
                }
                var usuario = _usuarioRepositorio.Selecionar(caixa.IdUsuario);
                if (usuario.PerfilUsuario != EnumPerfilUsuario.ADMINISTRADOR &&
                    usuario.PerfilUsuario != EnumPerfilUsuario.VENDEDOR)
                {
                    return BadRequest("Não é possível abrir o caixa para esse usuário.");
                }
                caixa.DataAbertura = DateTime.Now;
                _caixaRepositorio.Incluir(caixa);
                return Ok("O caixa foi aberto.");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        [Route("FecharCaixa/{id}")]
        [Authorize(Roles = "ADMINISTRADOR, VENDEDOR")]
        public ActionResult<List<FechamentoCaixaDTO>> Put(int id, [FromBody] Caixa caixa)
        {
            if (caixa == null)
            {
                return BadRequest("Não foi possível fechar o caixa, sem dados para o fechamento.");
            }

            if (id == caixa.Id)
            {
                var caixaBaseDeDados = _caixaRepositorio.Selecionar(id);
                if (caixaBaseDeDados.EstaAberto())
                {
                    foreach (var item in caixa.Descartes)
                    {
                        ProdutoFinal descarte = (ProdutoFinal)_produtoRepositorio.Selecionar(item.IdProdutoFinal);

                        Estoque estoqueDescarte = new Estoque()
                        {
                            Produto = descarte,
                            Data = DateTime.Now,
                            Quantidade = item.Quantidade,
                            TipoEstoque = EnumTipoEstoque.SAIDA
                        };
                        _estoqueRepositorio.Incluir(estoqueDescarte);
                        descarte.QuantidadeEstoque -= estoqueDescarte.Quantidade;
                        _produtoRepositorio.Alterar(descarte);
                    }

                    caixaBaseDeDados.DataFechameto = DateTime.Now;
                    caixaBaseDeDados.SituacaoCaixa = EnumSitucaoCaixa.FECHADO;
                    _caixaRepositorio.Alterar(caixaBaseDeDados);

                    var valores = _vendaRepositorio.VendaCaixa(caixa.Id);

                    return Ok(valores);
                }
            }
            return BadRequest("Impossível realizar o fechamento do caixa, pois o caixa não está aberto.");
        }
    }
}

