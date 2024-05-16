using AutoMapper;
using Dominio.DTOs;
using Dominio.Modelos;

namespace Servicios.Perfiles
{
    internal class PedidoExpresProfile : Profile
    {
        public PedidoExpresProfile()
        {
            CreateMap<PedidoExpresDTO, PedidoExpres>();
            CreateMap<PedidoExpres, PedidoExpresDTO>();
        }
    }
}