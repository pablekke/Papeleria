using AutoMapper;
using Dominio.DTOs;
using Dominio.Modelos;

namespace Servicios.Perfiles
{
    internal class PedidoComunProfile : Profile
    {
        public PedidoComunProfile()
        {
            CreateMap<PedidoComunDTO, PedidoComun>();
            CreateMap<PedidoComun, PedidoComunDTO>()
                .ForMember(dest => dest.Lineas, act => act.MapFrom(src => src.Lineas));
        }

    }
}