using Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace AccesoADatos
{
    public class RepositorioArticulo : Repositorio<Articulo>, IRepositorioArticulo
    {
        public RepositorioArticulo(DbContext contexto):base( contexto)
        {
            _contexto = contexto;
        }

        public bool ExisteCodigo(string codigo)
        {
            return _contexto.Set<Articulo>()
                .Any(a => a.Codigo == codigo);
        }
        public Articulo GetArticuloPorId(int id)
        {
            return _contexto.Set<Articulo>().FirstOrDefault(a => a.ArticuloId == id);
        }
        public bool ExisteNombre(string nombre)
        {
            return _contexto.Set<Articulo>()
                .Any(a => a.Nombre.ToLower() == nombre.ToLower());
        }
        public IEnumerable<Articulo> GetArticulosFiltrados(string nombre, double monto)
        {
            return _contexto.Set<Articulo>()
                           .Where(a => a.Nombre.Contains(nombre) && a.Precio >= monto)
                           .OrderBy(a => a.Nombre);
        }
    }
}
