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

        public CaixaController(ICaixaRepositorio caixaRepositorio, IUsuarioRepositorio usuarioRepositorio)

        {
            _caixaRepositorio = caixaRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }      

        [HttpPost]
        //[Route("AbrirCaixa")]
        //[Authorize(Roles = "ADMINISTRADOR, VENDEDOR")]
        public IActionResult Post([FromBody] Caixa caixa)
        {   

            try
            {
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
                _caixaRepositorio.Incluir(caixa);
                return Ok("O caixa foi aberto.");


            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut()]
        //[Route("FecharCaixa")]
        //[Authorize(Roles = "ADMINISTRADOR, VENDEDOR")]
        public ActionResult Put([FromBody] Caixa caixa)
        {
            if (_caixaRepositorio.VerificaExistenciaDeCaixaEmAberto())
            {

                caixa.SituacaoCaixa = EnumSitucaoCaixa.FECHADO;
                return Ok("O caixa foi fechado.");
            }
            return BadRequest("Impossível realizar o fechamento do caixa, pois não existe caixa aberto.");
        }      
    }
}
