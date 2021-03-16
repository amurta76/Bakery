using Bakery.Dominio;
using Bakery.Dominio.Dto;
using Bakery.Dominio.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Data.Interface
{
    public interface IProdutoRepositorio : IBaseRepositorio<Produto>
    {
        decimal BuscarQuantidadeEstoque(int id);
        List<ProdutoListagemDTO> ListarMateriaPrima(string nome, bool mostrarInativos,EnumTipoProduto tipoProduto);
        List<ProdutoFinalListagemDTO> ListarProdutosFinal(string nome, bool mostrarInativos, EnumTipoProduto tipoProduto);
         
    }
}
