using Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace AccesoADatos
{
    public class RepositorioPedidoComun: Repositorio<PedidoComun>, IRepositorioPedidoComun
    {
        public RepositorioPedidoComun(DbContext contexto) :base(contexto)
        { 
            _contexto = contexto;
        }

        public IEnumerable<PedidoComun> GetPedidosComunes()
        {
            return _contexto.Set<PedidoComun>()
                .Include(p => p.Cliente)
                .Include(p => p.Lineas)
                    .ThenInclude(l => l.Articulo)
                .OrderByDescending(pc => pc.FechaEmision);
        }
    }
}
