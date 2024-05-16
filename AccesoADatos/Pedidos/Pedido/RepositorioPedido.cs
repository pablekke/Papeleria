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

        public void AnularPedido(int id)
        {
            // Obtener el pedido por su ID
            var pedido = _contexto.Set<Pedido>().FirstOrDefault(p => p.PedidoId == id);

            // Verificar si el pedido existe
            if (pedido == null)
            {
                throw new ArgumentException($"No se encontró ningún pedido con el ID {id}");
            }

            // Marcar el pedido como anulado
            pedido.Anulado = true;

            // Guardar los cambios en la base de datos
            _contexto.SaveChanges();
        }

        public Pedido GetPedidoPorId(int id)
        {
            return _contexto.Set<Pedido>().FirstOrDefault(p => p.PedidoId == id);
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
        public IEnumerable<Pedido> GetPedidosNoEntregadosPorFecha(DateTime? fechaEmision)
        {
            //pedidos de la fecha argumento que no hayan sido entregados, ni anulados
            IEnumerable<Pedido> pedidos = _contexto.Set<Pedido>().Include(p => p.Cliente)
                                                    .Where(p => !p.Entregado && !p.Anulado);
            if (fechaEmision != null)
            {
                DateTime fecha = (DateTime)fechaEmision;
                pedidos = pedidos.Where(p => p.FechaEmision.Date == fecha.Date);
            }
            return pedidos.OrderBy(p => p.Cliente.Nombre);
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