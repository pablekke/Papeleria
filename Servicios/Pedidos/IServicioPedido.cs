using Dominio.DTOs;

namespace Servicios
{
    public interface IServicioPedido
    {
        IEnumerable<PedidoDTO> GetPedidos();
        IEnumerable<PedidoDTO> GetPedidosAnulados();
        IEnumerable<PedidoDTO> GetPedidosNoEntregadosPorFecha(DateTime fechaEmision);
        IEnumerable<PedidoDTO> GetPedidosConMontoSuperiorA(double monto);
    }
}