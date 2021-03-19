using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Dominio.Dto
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Senha { get; set; }

        public bool ValidaLogin() {
            return !(string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Senha));
        }
    }
}
