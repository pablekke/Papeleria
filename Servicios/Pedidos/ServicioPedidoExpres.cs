using AccesoADatos;
using AutoMapper;
using Dominio.DTOs;
using Dominio.Modelos;

namespace Servicios
{
    public class ServicioPedidoExpres : Servicio<PedidoExpres, PedidoExpresDTO>, IServicioPedidoExpres
    {
        private readonly IRepositorioPedidoExpres _repositorio;
        public ServicioPedidoExpres(IMapper mapeador, IRepositorioPedidoExpres repositorio) : base(mapeador, repositorio)
        {
            _repositorio = repositorio;
        }
        public IEnumerable<PedidoExpresDTO> GetPedidosExpres()
        {
            var pedidos = _repositorio.GetPedidosExpres();
            return _mapeador.Map<IEnumerable<PedidoExpresDTO>>(pedidos);
        }
    }
}
