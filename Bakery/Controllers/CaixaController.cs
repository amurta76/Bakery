using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bakery.Controllers
{
    [Route("api/[controller]")]
    public class CaixaController : Controller
    {
        private readonly ICaixaRepositorio _caixaRepositorio;
        public CaixaController(ICaixaRepositorio caixaRepositorio) {
            _caixaRepositorio = caixaRepositorio;
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
        [Authorize(Roles = "ADMINISTRADOR, VENDEDOR")]
        public IActionResult Post([FromBody]Caixa caixa)
        {
            try
            {
                _caixaRepositorio.Incluir(caixa);
                return Ok("Caixa Aberto");

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
