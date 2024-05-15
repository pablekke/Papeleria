using AutoMapper;
using Dominio.DTOs;
using Dominio.Modelos;

namespace Servicios.Perfiles
{
    internal class PedidoProfile : Profile
    {
        public PedidoProfile()
        {
            CreateMap<PedidoDTO, Pedido>();
            CreateMap<Pedido, PedidoDTO>();
        }
    }
}