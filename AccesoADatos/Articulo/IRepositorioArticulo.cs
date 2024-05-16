using Dominio.Modelos;

namespace AccesoADatos
{
    public interface IRepositorioArticulo : IRepositorio<Articulo>
    {
        Articulo GetArticuloPorId(int id);
        IEnumerable<Articulo> GetArticulosFiltrados(string nombre, double monto);
        bool ExisteCodigo(string codigo);
        bool ExisteNombre(string nombre);
    }
}