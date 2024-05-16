using Dominio.Modelos;

namespace AccesoADatos
{
    public interface IRepositorioCliente:IRepositorio<Cliente>
    {
        //cuando es "PorMonto" significa que son los clientes que tengan pedidos con algun monto superior a ese monto
        Cliente GetClientePorId(int id);
        IEnumerable<Cliente> GetClientesPorString(string texto);
        IEnumerable<Cliente> GetClientesPorMonto(double monto);
        IEnumerable<Cliente> GetClientesPorStringYMonto(string texto, double monto);
    }
}