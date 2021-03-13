using Bakery.Data.Interface;
using Bakery.Dominio;
using Bakery.Dominio.Dto;
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
    [Authorize(Roles = "ADMINISTRADOR")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet("{id}")]
        public ActionResult<Usuario> Get(int id)
        {
            try
            {
                var usuario = _usuarioRepositorio.Selecionar(id);

                if (usuario == null)
                {
                    return NotFound("Usuário não encontrado.");
                }

                return Ok(usuario);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Usuario usuario)
        {
            try
            {
                _usuarioRepositorio.Incluir(usuario);
                return Ok("Usuário incluído com sucesso.");
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Usuario usuario)
        {
            try
            {
                if (id == usuario.Id)
                {
                    _usuarioRepositorio.Alterar(usuario);
                    return Ok("Usuário alterado com sucesso.");
                }
                else
                    return BadRequest("Falha na alteração do usuário.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _usuarioRepositorio.Excluir(id);
                return Ok("Usuário deletado com sucesso.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet()]
        [Route("ListarUsuario")]
        public ActionResult<List<UsuarioDTO>> ListarUsuario(string nome, string email) 
        {
            try
            {
                var listausuario=_usuarioRepositorio.ListarUsuario(nome, email);
                return Ok(listausuario);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}

