using Dominio.Modelos;

namespace AccesoADatos
{
    public interface IRepositorioPedido : IRepositorio<Pedido>
    {
        IEnumerable<Pedido> GetPedidos();
        IEnumerable<Pedido> GetPedidosAnulados();
        IEnumerable<Pedido> GetPedidosNoEntregadosPorFecha(DateTime fechaEmision);
        IEnumerable<Pedido> PedidosConMontoSuperiorA(double monto);
    }
}