using Bakery.Data.Interface;
using Bakery.Data.Repositorio;
using Bakery.Dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bakery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstoqueController : ControllerBase
    {
        private readonly IEstoqueRepositorio _estoqueRepositorio;
        private readonly IProdutoRepositorio _produtoRepositorio;

        public EstoqueController(IEstoqueRepositorio estoqueRepositorio, IProdutoRepositorio produtoRepositorio)

        {
            _estoqueRepositorio = estoqueRepositorio;
            _produtoRepositorio = produtoRepositorio;
        }

        [HttpGet("{id}")]
        public ActionResult<Estoque> Get(int id)
        {
            try
            {
                var estoque = _estoqueRepositorio.Selecionar(id);
                if (estoque == null)
                    return NotFound();
                return Ok(estoque);

            }
            catch
            {

                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }

        // POST api/<EstoqueController>
        [HttpPost]
        public IActionResult Post([FromBody] Estoque estoque)
        {
            try
            {
                if (estoque.Quantidade <= 0)
                    return BadRequest("Quantidade inválida");

                var produto = _produtoRepositorio.Selecionar(estoque.IdProduto);

                if (produto == null)
                    return NotFound("Produto não encontrado");

                if (estoque.TipoEstoque == Dominio.Enum.EnumTipoEstoque.ENTRADA)
                    produto.QuantidadeEstoque += estoque.Quantidade;

                else produto.QuantidadeEstoque -= estoque.Quantidade;

                _estoqueRepositorio.Incluir(estoque);
                return Ok("Estoque alterado com sucesso");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }


    }
}
