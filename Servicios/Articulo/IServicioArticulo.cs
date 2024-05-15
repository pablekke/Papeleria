using Dominio.DTOs;

namespace Servicios
{
    public interface IServicioArticulo : IServicio<ArticuloDTO>
    {
        /// <summary>
        /// Obtiene una lista de artículos filtrados por el nombre.
        /// </summary>
        /// <param name="nombre">Nombre parcial o completo para filtrar los artículos.</param>
        /// <returns>Una colección de ArticuloDTO.</returns>
        IEnumerable<ArticuloDTO> GetArticulosFiltrados(string nombre, double monto);
        ArticuloDTO GetArticuloPorId(int id);
    }
}
