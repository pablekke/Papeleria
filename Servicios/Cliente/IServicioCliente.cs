using Dominio.DTOs;

namespace Servicios
{
    public interface IServicioCliente : IServicio<ClienteDTO>
    {
        //cuando es "PorMonto" significa que son los clientes que tengan pedidos con algun monto superior a ese monto
        ClienteDTO GetClientePorId(int id);
        IEnumerable<ClienteDTO> GetClientesPorString(string texto);
        IEnumerable<ClienteDTO> GetClientesPorMonto(double monto);
        IEnumerable<ClienteDTO> GetClientesPorStringYMonto(string texto, double monto);

    }
}