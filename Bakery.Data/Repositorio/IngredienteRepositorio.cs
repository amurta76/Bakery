﻿using Bakery.Data.Interface;
using Bakery.Data.Repository;
using Bakery.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Data.Repositorio
{
    public class IngredienteRepositorio : BaseRepositorio<Ingrediente>, IIngredienteRepositorio
    {
        public IngredienteRepositorio(Contexto contexto) : base(contexto)
        {

        }
    }
}
