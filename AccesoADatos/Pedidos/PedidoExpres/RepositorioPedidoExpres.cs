using Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace AccesoADatos
{
    public class RepositorioPedidoExpres : Repositorio<PedidoExpres>, IRepositorioPedidoExpres
    {
        public RepositorioPedidoExpres(DbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public IEnumerable<PedidoExpres> GetPedidosExpres()
        {
            return _contexto.Set<PedidoExpres>()
                .Include(p => p.Cliente)
                .Include(p => p.Lineas)
                    .ThenInclude(l => l.Articulo)
                .OrderByDescending(pc => pc.FechaEmision);
        }
    }
}