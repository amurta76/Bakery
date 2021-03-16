using Bakery.Data.Interface;
using Bakery.Dominio;
using Bakery.Dominio.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
        [ProducesResponseType(200)] // Ok
        [ProducesResponseType(401)] //Não autorizado
        [ProducesResponseType(403)] //Proibido
        [ProducesResponseType(404)] //Não encontrado
        [ProducesResponseType(500)] //Erro interno do servidor
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
        [ProducesResponseType(200)] // Ok
        [ProducesResponseType(401)] //Não autorizado
        [ProducesResponseType(403)] //Proibido        
        [ProducesResponseType(500)] //Erro interno do servidor
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
        [ProducesResponseType(200)] // Ok
        [ProducesResponseType(400)] //Requisição inválida
        [ProducesResponseType(401)] //Não autorizado
        [ProducesResponseType(403)] //Proibido        
        [ProducesResponseType(500)] //Erro interno do servidor
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
        [ProducesResponseType(200)] // Ok        
        [ProducesResponseType(401)] //Não autorizado
        [ProducesResponseType(403)] //Proibido        
        [ProducesResponseType(500)] //Erro interno do servidor
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
        [ProducesResponseType(200)] // Ok        
        [ProducesResponseType(401)] //Não autorizado
        [ProducesResponseType(403)] //Proibido        
        [ProducesResponseType(500)] //Erro interno do servidor
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

