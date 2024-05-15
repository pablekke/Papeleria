using Dominio.Excepciones;
using Microsoft.EntityFrameworkCore;

namespace AccesoADatos
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        protected DbContext _contexto { get; set; }
        protected DbSet<T> _entidad { get; }
        public Repositorio(DbContext contexto)
        {
            _contexto = contexto;
            _entidad = contexto.Set<T>();
        }

        #region CRUD
        public void Crear(T item)
        {
            _contexto.Set<T>().Add(item);
            _contexto.SaveChanges();
        }

        public void Borrar(int id)
        {
            var item = GetPorId(id);
            _entidad.Remove(item);
            _contexto.SaveChanges();
        }

        public void Actualizar(T item)
        {
            _contexto.Entry(item).State = EntityState.Modified;
            _contexto.SaveChanges();
        }

        public T GetPorId(int id)
        {
            var item = _entidad.Find(id);
            if (item == null)
            {
                throw new ExcepcionElementoInexistente("No existe usuario con ese identificador");
            }
            return _entidad.Find(id);
        }
        #endregion
    }
}
