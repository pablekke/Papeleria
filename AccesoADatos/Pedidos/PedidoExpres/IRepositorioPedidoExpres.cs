using Dominio.Modelos;

namespace AccesoADatos
{
    public interface IRepositorioPedidoExpres : IRepositorio<PedidoExpres>
    {
        IEnumerable<PedidoExpres> GetPedidosExpres();
    }
}
