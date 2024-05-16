using Dominio.DTOs;

namespace Servicios
{
    public interface IServicioArticulo : IServicio<ArticuloDTO>
    {
        IEnumerable<ArticuloDTO> GetArticulosFiltrados(string nombre, double monto);
        ArticuloDTO GetArticuloPorId(int id);
    }
}