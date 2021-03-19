using Bakery.Dominio.Enum;
using Bakery.Dominio.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Dominio
{
    public class Usuario : IEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public EnumPerfilUsuario PerfilUsuario { get; set; }
        public string Senha { get; set; }
        public List<Caixa> Caixas { get; set; }
    }
}
