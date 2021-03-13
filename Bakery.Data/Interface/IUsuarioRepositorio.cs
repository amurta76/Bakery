using Bakery.Dominio;
using Bakery.Dominio.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Data.Interface
{
    public interface IUsuarioRepositorio : IBaseRepositorio<Usuario>
    {
        List<UsuarioDTO> ListarUsuario(string nome, string email);

        Usuario Login(LoginDTO login);
    }
}
