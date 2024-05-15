using AccesoADatos;
using AutoMapper;
using Dominio.DTOs;
using Dominio.Modelos;


namespace Servicios
{
    public class ServicioPedidoComun : Servicio<PedidoComun, PedidoComunDTO>, IServicioPedidoComun
    {
        private readonly IRepositorioPedidoComun _repositorio;
        public ServicioPedidoComun(IMapper mapeador, IRepositorioPedidoComun repositorio) : base(mapeador, repositorio)
        {
            _repositorio = repositorio;
        }

        public IEnumerable<PedidoComunDTO> GetPedidosComunes()
        {
            var pedidos = _repositorio.GetPedidosComunes();
            return _mapeador.Map<IEnumerable<PedidoComunDTO>>(pedidos);
        }
    }
}
