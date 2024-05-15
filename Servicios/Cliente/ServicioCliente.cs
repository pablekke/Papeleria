using AccesoADatos;
using AutoMapper;
using Dominio.DTOs;
using Dominio.Modelos;

namespace Servicios
{
    public class ServicioCliente : Servicio<Cliente, ClienteDTO>, IServicioCliente
    {
        private readonly IRepositorioCliente _repositorio;
        public ServicioCliente(IMapper mapeador, IRepositorioCliente repositorio) : base(mapeador, repositorio)
        {
            _repositorio = repositorio;
        }

        public ClienteDTO GetClientePorId(int id)
        {
            var cliente = _repositorio.GetClientePorId(id);
            return _mapeador.Map<ClienteDTO>(cliente);
        }

        public IEnumerable<ClienteDTO> GetClientesPorMonto(double monto)
        {
            var clientes = _repositorio.GetClientesPorMonto(monto);
            return _mapeador.Map<IEnumerable<ClienteDTO>>(clientes);
        }

        public IEnumerable<ClienteDTO> GetClientesPorString(string texto)
        {
            var clientes = _repositorio.GetClientesPorString(texto);
            return _mapeador.Map<IEnumerable<ClienteDTO>>(clientes);
        }

        public IEnumerable<ClienteDTO> GetClientesPorStringYMonto(string texto, double monto)
        {
            var clientes = _repositorio.GetClientesPorStringYMonto(texto, monto);
            return _mapeador.Map<IEnumerable<ClienteDTO>>(clientes);
        }
    }
}