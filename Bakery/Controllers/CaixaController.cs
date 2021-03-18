using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakery.Data.Interface;
using Bakery.Dominio;
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

        public CaixaController(ICaixaRepositorio caixaRepositorio, IUsuarioRepositorio usuarioRepositorio,
                                        IEstoqueRepositorio estoqueRepositorio, IProdutoRepositorio produtoRepositorio)

        {
            _caixaRepositorio = caixaRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _estoqueRepositorio = estoqueRepositorio;
            _produtoRepositorio = produtoRepositorio;
        }      

        [HttpPost]
        //[Route("AbrirCaixa")]
        //[Authorize(Roles = "ADMINISTRADOR, VENDEDOR")]
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

        [HttpPut("{id}")]
        //[Route("FecharCaixa")]
        //[Authorize(Roles = "ADMINISTRADOR, VENDEDOR")]
        public ActionResult<decimal> Put(int id,[FromBody] Caixa caixa)
        {
            if (caixa == null)
            {
                return BadRequest("Não foi possível abrir o caixa, sem dados para a abertura.");
            }

            if (id == caixa.Id)
            {
                var caixaBaseDeDados = _caixaRepositorio.Selecionar(id);
                if (caixaBaseDeDados.EstaAberto())
                {
                    caixaBaseDeDados.DataFechameto = DateTime.Now;
                    caixaBaseDeDados.SituacaoCaixa = EnumSitucaoCaixa.FECHADO;
                    _caixaRepositorio.Alterar(caixaBaseDeDados);

                    foreach (var item in caixa.Descartes)
                    {
                        ProdutoFinal descarte = (ProdutoFinal)_produtoRepositorio.Selecionar(item.IdProdutoFinal);
                                              
                        Estoque estoqueDescarte = new Estoque()
                        {
                            Produto = descarte,
                            Data = new DateTime(),
                            Quantidade = item.Quantidade,
                            TipoEstoque = EnumTipoEstoque.SAIDA
                        };
                        _estoqueRepositorio.Incluir(estoqueDescarte);
                        descarte.QuantidadeEstoque -= estoqueDescarte.Quantidade;
                        _produtoRepositorio.Alterar(descarte);
                    }

                    
                    
                    return Ok("O caixa foi fechado.");
                }                
            }
            return BadRequest("Impossível realizar o fechamento do caixa, pois o caixa não está aberto.");
        }      
    }
}
