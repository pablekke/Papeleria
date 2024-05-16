using Dominio.DTOs;

namespace Servicios
{
    public interface IServicioPedido : IServicio<PedidoDTO>
    {
        PedidoDTO GetPedidoPorId(int id);
        IEnumerable<PedidoDTO> GetPedidos();
        void AnularPedido(int id);
        IEnumerable<PedidoDTO> GetPedidosAnulados();
        IEnumerable<PedidoDTO> GetPedidosNoEntregadosPorFecha(DateTime? fechaEmision);
        IEnumerable<PedidoDTO> GetPedidosConMontoSuperiorA(double monto);
        
    }
}