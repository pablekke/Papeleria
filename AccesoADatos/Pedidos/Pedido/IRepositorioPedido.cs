using Dominio.Modelos;

namespace AccesoADatos
{
    public interface IRepositorioPedido : IRepositorio<Pedido>
    {
        Pedido GetPedidoPorId(int id);
        void AnularPedido(int id);
        IEnumerable<Pedido> GetPedidos();
        IEnumerable<Pedido> GetPedidosAnulados();
        IEnumerable<Pedido> GetPedidosNoEntregadosPorFecha(DateTime? fechaEmision);
        IEnumerable<Pedido> PedidosConMontoSuperiorA(double monto);
    }
}