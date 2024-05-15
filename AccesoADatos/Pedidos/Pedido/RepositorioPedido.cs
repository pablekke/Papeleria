using Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace AccesoADatos
{
    public class RepositorioPedido: Repositorio<Pedido>, IRepositorioPedido
    {
        public RepositorioPedido(DbContext contexto) :base(contexto)
        { 
            _contexto = contexto;
        }

        public IEnumerable<Pedido> GetPedidos()
        {
            return _contexto.Set<Pedido>().ToList();
        }

        public IEnumerable<Pedido> GetPedidosAnulados()
        {
            return _contexto.Set<Pedido>()
                .Include(p => p.Cliente)
                .Include(p => p.Lineas)
                    .ThenInclude(l => l.Articulo) //incluye el articulo de cada LineaPedido
                .Where(p => p.Anulado)
                .OrderByDescending(p => p.FechaEmision);
        }
        public IEnumerable<Pedido> GetPedidosNoEntregadosPorFecha(DateTime fechaEmision)
        {
            return _contexto.Set<Pedido>()
                            //pedidos de la fecha argumento que no hayan sido entregados, ni anulados
                            .Where(p => p.FechaEmision.Date == fechaEmision.Date &&
                                       !p.Entregado &&
                                       !p.Anulado)
                            .Include(p => p.Cliente);
        }

        public IEnumerable<Pedido> PedidosConMontoSuperiorA(double monto)
        {
            List<Pedido> pedidos = _contexto.Set<Pedido>().Where(p => p.Total >= monto).Include(p => p.Cliente).ToList();
            if (pedidos.Count != 0)
            {
                pedidos.OrderBy(p => p.Cliente.Nombre);
            }
            return pedidos;      
        }
    }
}