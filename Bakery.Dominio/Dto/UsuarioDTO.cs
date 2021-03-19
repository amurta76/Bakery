using Bakery.Dominio.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Dominio.Dto
{
    public class UsuarioDTO
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string PerfilUsuario { get; set; }
    }
}
