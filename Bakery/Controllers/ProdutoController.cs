using Microsoft.AspNetCore.Mvc;
using System;
using Bakery.Data.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakery.Dominio;
using Microsoft.AspNetCore.Http;


namespace Bakery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepositorio _produtoRepositorio;

        public ProdutoController(IProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        [HttpGet("{id}")]
        public ActionResult<Produto> Get(int id)
        {
            try
            {
                var produto = _produtoRepositorio.Selecionar(id);

                if (produto == null)
                {
                    return NotFound("Produto não encontrado.");
                }

                return Ok(produto);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Produto produto)
        {
            try
            {
                if (produto.QuantidadeEstoque <= 0)
                {
                    return BadRequest("A quantidade não pode ser negativa ou zero.");
                }

                _produtoRepositorio.Incluir(produto);
                return Ok("Produto incluído com sucesso.");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Produto produto)
        {
            try
            {
                if (id == produto.Id)
                {
                    _produtoRepositorio.Alterar(produto);

                    if (produto.QuantidadeEstoque <= 0)
                    {
                        return BadRequest("A quantidade não pode ser negativa ou zero.");
                    }
                   
                    _produtoRepositorio.Alterar(produto);
                    return Ok("Produto alterado com sucesso.");                    
                }
                else
                    return BadRequest("Falha na alteração do produto.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return BadRequest("Não é permitido a exclusão de matérias-primas.");
        }
    }
}
