using AccesoADatos;
using AutoMapper;
using Dominio.DTOs;
using Dominio.Modelos;

namespace Servicios
{
    public class ServicioPedido : Servicio<Pedido, PedidoDTO>, IServicioPedido
    {
        private readonly IRepositorioPedido _repositorio;
        public ServicioPedido(IMapper mapeador, IRepositorioPedido repositorio) : base(mapeador, repositorio)
        {
            _repositorio = repositorio;
        }

        public void AnularPedido(int id)
        {
            _repositorio.AnularPedido(id);
        }

        public PedidoDTO GetPedidoPorId(int id)
        {
            var pedido = _repositorio.GetPedidoPorId(id);
            return _mapeador.Map<PedidoDTO>(pedido);
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

        public IEnumerable<PedidoDTO> GetPedidosNoEntregadosPorFecha(DateTime? fechaEmision)
        {
            var pedidos = _repositorio.GetPedidosNoEntregadosPorFecha(fechaEmision);
            return _mapeador.Map<IEnumerable<PedidoDTO>>(pedidos);
        }
    }
}