using Dominio.DTOs;

namespace Servicios
{
    public interface IServicioPedidoExpres : IServicio<PedidoExpresDTO>
    {
        IEnumerable<PedidoExpresDTO> GetPedidosExpres();
    }
}