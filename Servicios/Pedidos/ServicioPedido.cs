using AccesoADatos;
using AutoMapper;
using Dominio.DTOs;

namespace Servicios
{
    public class ServicioPedido : IServicioPedido
    {
        private readonly IRepositorioPedido _repositorio;
        private readonly IMapper _mapeador;
        public ServicioPedido(IMapper mapeador, IRepositorioPedido repositorio)
        {
            _repositorio = repositorio;
            _mapeador = mapeador;
        }
        public IEnumerable<PedidoDTO> GetPedidos()
        {
            var pedidos = _repositorio.GetPedidos();
            return _mapeador.Map<IEnumerable<PedidoDTO>>(pedidos);
        }

        public IEnumerable<PedidoDTO> GetPedidosAnulados()
        {
            var pedidos = _repositorio.GetPedidosAnulados();
            return _mapeador.Map<IEnumerable<PedidoDTO>>(pedidos);
        }

        public IEnumerable<PedidoDTO> GetPedidosConMontoSuperiorA(double monto)
        {
            var pedidos = _repositorio.PedidosConMontoSuperiorA(monto);
            return _mapeador.Map<IEnumerable<PedidoDTO>>(pedidos);
        }

        public IEnumerable<PedidoDTO> GetPedidosNoEntregadosPorFecha(DateTime fechaEmision)
        {
            var pedidos = _repositorio.GetPedidosNoEntregadosPorFecha(fechaEmision);
            return _mapeador.Map<IEnumerable<PedidoDTO>>(pedidos);
        }
    }
}