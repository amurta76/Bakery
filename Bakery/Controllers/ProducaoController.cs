using Bakery.Data.Interface;
using Bakery.Dominio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducaoController : ControllerBase
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IEstoqueRepositorio _estoqueRepositorio;
        private readonly IIngredienteRepositorio _ingredienteRepositorio;

        public ProducaoController(IProdutoRepositorio produtoRepositorio, IEstoqueRepositorio estoqueRepositorio,
                                                                        IIngredienteRepositorio ingredienteRepositorio)
        {
            _estoqueRepositorio = estoqueRepositorio;
            _produtoRepositorio = produtoRepositorio;
            _ingredienteRepositorio = ingredienteRepositorio;
        }

       [HttpPost]
        public ActionResult<Produto> Post([FromBody] ProdutoFinalProduzido produto, Ingrediente ingrediente, Estoque estoque)
        {
            foreach (var item in produto.Receita)
            {
                if (produto.)
                {

                }
                item.
            }
        }

    }
}
