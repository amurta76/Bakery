using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Dominio.Dto
{
    public class LoginDTO
    {
        public string email { get; set; }
        public string Senha { get; set; }

        public bool ValidaLogin() {
            return !(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(Senha));
        }
    }
}
