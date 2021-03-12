using Bakery.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Data.Interface
{
    public interface IUsuarioRepositorio : IBaseRepositorio<Usuario>
    {
        List<Usuario> ListarUsuario(string nome, string email);
    }
}
