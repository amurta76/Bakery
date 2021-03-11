using Bakery.Data.Interface;
using Bakery.Data.Repository;
using Bakery.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Data.Repositorio
{
    public class UsuarioReposotorio : BaseRepositorio<Usuario>, IUsuarioRepositorio
    {
        public UsuarioReposotorio(Contexto contexto) : base(contexto)
        {

        }
    }
}
