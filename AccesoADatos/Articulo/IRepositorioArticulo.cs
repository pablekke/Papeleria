using Dominio.Modelos;

namespace AccesoADatos
{
    /// <summary>
    /// Define la interfaz para el repositorio de artículos, incluyendo operaciones específicas
    /// para artículos además de las operaciones CRUD genéricas.
    /// </summary>
    public interface IRepositorioArticulo : IRepositorio<Articulo>
    {
        /// <summary>
        /// Obtiene una lista de artículos que coinciden con el nombre especificado.
        /// </summary>
        /// <param name="nombre">El nombre o parte del nombre por el que filtrar los artículos.</param>
        /// <returns>Una colección de objetos Articulo.</returns>
        IEnumerable<Articulo> GetArticulosFiltrados(string nombre, double monto);
        /// <summary>
        /// Obtiene una lista de artículos que coinciden con el nombre especificado.
        /// </summary>
        /// <param name="nombre">El nombre o parte del nombre por el que filtrar los artículos.</param>
        /// <returns>Una colección de objetos Articulo.</returns>
        Articulo GetArticuloPorId(int id);

        /// <summary>
        /// Verifica si ya existe un artículo con el código especificado.
        /// </summary>
        /// <param name="codigo">El código del artículo a verificar.</param>
        /// <returns>True si el artículo existe, de lo contrario false.</returns>
        bool ExisteCodigo(string codigo);

        /// <summary>
        /// Verifica si ya existe un artículo con el mismo nombre especificado.
        /// </summary>
        /// <param name="nombre">El nombre del artículo a verificar.</param>
        /// <returns>True si el artículo existe, de lo contrario false.</returns>
        bool ExisteNombre(string nombre);
    }
}
