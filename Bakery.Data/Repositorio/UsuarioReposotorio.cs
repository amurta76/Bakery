using Bakery.Data.Interface;
using Bakery.Data.Repository;
using Bakery.Dominio;
using Bakery.Dominio.Dto;
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

        public List<UsuarioDTO> ListarUsuario(string nome, string email)
        {
            List<Usuario> listausuario = new List<Usuario>(0);
            if (string.IsNullOrEmpty(nome) && string.IsNullOrEmpty(email))
            {
                listausuario = _contexto.Set<Usuario>().ToList();
            }
            else
            {
                listausuario = _contexto.Set<Usuario>().Where(u => u.Nome == nome || u.Email == email).ToList();
            }

            return listausuario.Select(s =>

                    new UsuarioDTO()
                    {
                        Nome = s.Nome,
                        Email = s.Email,
                        DataNascimento = s.DataNascimento,
                        PerfilUsuario = s.PerfilUsuario.ToString()
                    }

                ).OrderBy(x => x.Nome).ToList();
        }

        Usuario IUsuarioRepositorio.Login(LoginDTO login)
        {
            var usuario = _contexto.Set<Usuario>().FirstOrDefault(u => u.Email == login.email && u.Senha == login.Senha);
            return usuario;
        }
    }
}
