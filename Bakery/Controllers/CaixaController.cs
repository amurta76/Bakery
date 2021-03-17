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


        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        ////   [Authorize(Roles = "ADMINISTRADOR, VENDEDOR")]
        public IActionResult Post([FromBody] Caixa caixa)
        {
            try
            {
                if (caixa == null) {
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
                _caixaRepositorio.Incluir(caixa);
                return Ok("O caixa foi aberto.");

                
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
