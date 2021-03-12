using Bakery.Data.Interface;
using Bakery.Dominio.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bakery.Data.Repository
{
    public class BaseRepositorio<T> :IBaseRepositorio<T> where T : class, IEntity
    {
        protected readonly Contexto _contexto;

        public BaseRepositorio(Contexto contexto)
        {
            _contexto = contexto;
        }

        public void Incluir(T entity)
        {
            _contexto.Set<T>().Add(entity);
            _contexto.SaveChanges();
        }

        public void Alterar(T entity)
        {
            _contexto.Set<T>().Update(entity);
            _contexto.SaveChanges();
        }

        public T Selecionar(int id)
        {
            return _contexto.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public void Excluir(int id)
        {
            var entity = Selecionar(id);
            if(entity != null)
            {
                _contexto.Set<T>().Remove(entity);
                _contexto.SaveChanges();
            }
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }
    }
}
