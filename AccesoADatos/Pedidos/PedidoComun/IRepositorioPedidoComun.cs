using Dominio.Modelos;

namespace AccesoADatos
{
    public interface IRepositorioPedidoComun : IRepositorio<PedidoComun>
    {
        IEnumerable<PedidoComun> GetPedidosComunes();
    }
}