using AutoMapper;
using Dominio.DTOs;
using Dominio.Modelos;

namespace Servicios.Perfiles
{
    internal class LineaPedidoProfile: Profile
    {
        public LineaPedidoProfile()
        {
            CreateMap<LineaPedidoDTO, LineaPedido>();
            CreateMap<LineaPedido, LineaPedidoDTO>();
        }
    }
}
