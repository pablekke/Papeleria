using Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace AccesoADatos {
    public class RepositorioCliente : Repositorio<Cliente>, IRepositorioCliente
    {
        private readonly IRepositorioPedido _repositorioPedido;

        public RepositorioCliente(DbContext contexto, IRepositorioPedido repositorioPedido) : base(contexto)
        {
            _contexto = contexto;
            _repositorioPedido = repositorioPedido;
        }

        public Cliente GetClientePorId(int id)
        {
            return _contexto.Set<Cliente>().
                FirstOrDefault(c => c.ClienteId == id);
        }

        public IEnumerable<Cliente> GetClientesPorMonto(double monto)
        {
            //el new HashSet<Cliente> elimina los clientes duplicados
            var clientes = new HashSet<Cliente>();
            var pedidos = _repositorioPedido.PedidosConMontoSuperiorA(monto);
            foreach (var pedido in pedidos)
            {
                clientes.Add(pedido.Cliente);
            }
            return clientes.OrderBy(c => c.Nombre);
        }

        public IEnumerable<Cliente> GetClientesPorString(string texto)
        {
            return _contexto.Set<Cliente>().
                Where(c => c.Nombre.ToLower().Contains(texto.ToLower()) ||
                           c.Email.ToLower().Contains(texto.ToLower()) ||
                           c.Apellido.ToLower().Contains(texto.ToLower()))
                .OrderBy(c => c.Nombre).ToList();
        }

        public IEnumerable<Cliente> GetClientesPorStringYMonto(string texto, double monto)
        {
            //el new HashSet<Cliente> elimina los clientes duplicados
            var clientes = new HashSet<Cliente>(GetClientesPorString(texto));
            var pedidos = _repositorioPedido.PedidosConMontoSuperiorA(monto);

            var clientesFiltrados = pedidos
                            .Where(p => clientes.Contains(p.Cliente))
                            .Select(p => p.Cliente)
                            .Distinct();
            return clientesFiltrados.OrderBy(c => c.Nombre);
        }
    }
}
