using Microsoft.AspNetCore.Mvc;
using System;
using Bakery.Data.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakery.Dominio;
using Microsoft.AspNetCore.Http;
using Bakery.Dominio.Enum;
using Microsoft.AspNetCore.Authorization;
using Bakery.Dominio.Dto;

namespace Bakery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepositorio _produtoRepositorio;

        private readonly IEstoqueRepositorio _estoqueRepositorio;
        private readonly IIngredienteRepositorio _ingredienteRepositorio;
        private object mostrarInativos;

        public ProdutoController(IProdutoRepositorio produtoRepositorio,
                                 IEstoqueRepositorio estoqueRepositorio,
                                 IIngredienteRepositorio ingredienteRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
            _estoqueRepositorio = estoqueRepositorio;
            _ingredienteRepositorio = ingredienteRepositorio;
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

        [HttpPut()]
        [Route("Inativar/{id}")]
        [Authorize(Roles = "ADMINISTRADOR, ESTOQUISTA")]
        public IActionResult Inativar(int id, [FromBody] Produto produto)
        {
            try
            {
                if (id == produto.Id)
                {
                    if (produto.TipoProduto == EnumTipoProduto.MATERIA_PRIMA)
                    {
                        if (!_ingredienteRepositorio.MateriaPrimaSemProdutoFinal(produto.Id))
                        {
                            return BadRequest("Materia Prima utlizada em Produtos Finais Produzidos. Não pode ser inativada.");
                        }
                    }

                    produto.Situacao = false;
                    _produtoRepositorio.Alterar(produto);
                    return Ok("Produto inativado com sucesso.");
                }
                else
                    return BadRequest("Falha na inativação do produto.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMINISTRADOR, ESTOQUISTA")]
        public IActionResult Delete(int id)
        {
            return BadRequest("Não é permitido a exclusão de matérias-primas.");
        }


        #region MateriaPrima
        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR, ESTOQUISTA")]
        public IActionResult Post([FromBody] Produto produto)
        {
            return IncluirProduto(produto);
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "ADMINISTRADOR, ESTOQUISTA")]
        public IActionResult Put(int id, [FromBody] Produto produto)
        {
            return AlterarProduto(id, produto);
        }
        #endregion

        #region ProdutoFinal

        [HttpPut()]
        [Route("Final/{id}")]
        [Authorize(Roles = "ADMINISTRADOR, ESTOQUISTA")]
        public IActionResult Put(int id, [FromBody] ProdutoFinal produto)
        {
            return AlterarProdutoFinal(id, produto);
        }

        [HttpPost]
        [Route("Final")]
        [Authorize(Roles = "ADMINISTRADOR, ESTOQUISTA")]
        public IActionResult Post([FromBody] ProdutoFinal produto)
        {
            return IncluirProdutoFinal(produto);
        }

        #endregion

        #region ProdutoFinalProduzido

        [HttpPut()]
        [Route("FinalProduzido/{id}")]
        [Authorize(Roles = "ADMINISTRADOR, ESTOQUISTA")]
        public IActionResult Put(int id, [FromBody] ProdutoFinalProduzido produto)
        {
            if (VerificarEstoqueMateriaPrima(produto))
            {
                return AlterarProdutoFinal(id, produto);
            }
            else
                return BadRequest("Existem matérias - primas da receita que estão inativas ou com quantidades inválidas.");
        }

        [HttpPost]
        [Route("FinalProduzido")]
        [Authorize(Roles = "ADMINISTRADOR, ESTOQUISTA")]
        public IActionResult Post([FromBody] ProdutoFinalProduzido produto)
        {
            if (VerificarEstoqueMateriaPrima(produto))
            {
                return IncluirProdutoFinal(produto);
            }
            else
                return BadRequest("Existem matérias - primas da receita que estão inativas ou com quantidades inválidas.");
        }
        [HttpGet()]
        [Route("ListarMateriasPrimas")]
        public ActionResult<List<ProdutoListagemDTO>> ProdutoListagem(string nome,bool mostrarInativos)
        {
            try
            {
                var listarMateriaPrima = _produtoRepositorio.ListarMateriaPrima(nome,mostrarInativos,EnumTipoProduto.MATERIA_PRIMA );
                return Ok(listarMateriaPrima);
            }
            catch (Exception e) 
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private ActionResult<List<ProdutoListagemDTO>> Ok(Func<string, string, string, ActionResult<List<ProdutoListagemDTO>>> produtoListagem)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private

        private IActionResult IncluirProduto(Produto produto)
        {
            try
            {
                if (!produto.ValidaQuantidadeEstoque())
                {
                    return BadRequest("A quantidade não pode ser negativa ou zero.");
                }

                produto.Situacao = true;
                _produtoRepositorio.Incluir(produto);

                Estoque estoque = new Estoque()
                {
                    Produto = produto,
                    Data = new DateTime(),
                    Quantidade = produto.QuantidadeEstoque,
                    TipoEstoque = EnumTipoEstoque.ENTRADA
                };

                _estoqueRepositorio.Incluir(estoque);

                return Ok("Produto incluído com sucesso.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private IActionResult IncluirProdutoFinal(ProdutoFinal produto)
        {
            if (produto.ValidaQuantidadeEstoque() && produto.ValidaValor())
                return IncluirProduto(produto);
            else if (!produto.ValidaQuantidadeEstoque())
                return BadRequest("Quantidade inválida.");
            else
                return BadRequest("Valor inválido.");
        }

        private IActionResult AlterarProduto(int id, Produto produto)
        {
            try
            {
                if (id == produto.Id)
                {
                    var quantidadeEstoque = _produtoRepositorio.BuscarQuantidadeEstoque(id);

                    //a quantidade nao deve atualizar com o que foi informado
                    produto.QuantidadeEstoque = quantidadeEstoque;
                    produto.Situacao = true;
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

        private IActionResult AlterarProdutoFinal(int id, ProdutoFinal produto)
        {
            if (produto.ValidaQuantidadeEstoque() && produto.ValidaValor())
                return AlterarProduto(id, produto);
            else if (!produto.ValidaQuantidadeEstoque())
                return BadRequest("Quantidade inválida.");
            else
                return BadRequest("Valor inválido.");
        }

        private bool VerificarEstoqueMateriaPrima(ProdutoFinalProduzido produtoFinalProduzido)
        {
            if (produtoFinalProduzido.Receita == null)
                return false;

            foreach (var item in produtoFinalProduzido.Receita)
            {
                var ingrediente = _produtoRepositorio.Selecionar(item.IdMateriaPrima);

                if (!ingrediente.Situacao || item.ValidaQuantidade())
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

    }
}
