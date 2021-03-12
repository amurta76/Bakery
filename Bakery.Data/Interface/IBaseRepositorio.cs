using Bakery.Dominio.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Data.Interface
{
    public interface IBaseRepositorio<T> where T : class, IEntity
    {
        void Incluir(T entity);
        void Alterar(T entity);
        T Selecionar(int id);
        void Excluir(int id);
        void Dispose();

    }
}
