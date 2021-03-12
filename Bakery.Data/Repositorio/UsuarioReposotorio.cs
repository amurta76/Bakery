using Bakery.Data.Interface;
using Bakery.Data.Repository;
using Bakery.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bakery.Data.Repositorio
{
    public class UsuarioReposotorio : BaseRepositorio<Usuario>, IUsuarioRepositorio
    {
        public UsuarioReposotorio(Contexto contexto) : base(contexto)
        {

        }

        public List<Usuario> ListarUsuario(string nome, string email)
        {
            List<Usuario> listausuario;
            if (string.IsNullOrEmpty(nome) && string.IsNullOrEmpty(email))
            {
                listausuario = _contexto.Set<Usuario>().ToList();

            }
            else
            {
                listausuario = _contexto.Set<Usuario>().Where(u => u.Nome == nome || u.Email == email).ToList();
            }
            return listausuario;

        }
    }
}
