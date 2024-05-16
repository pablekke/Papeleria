using Dominio.DTOs;

namespace Servicios
{
    public interface IServicioPedidoComun : IServicio<PedidoComunDTO>
    {
        IEnumerable<PedidoComunDTO> GetPedidosComunes();
    }
}