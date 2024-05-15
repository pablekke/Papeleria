using AutoMapper;
using Dominio.Modelos;
using Dominio.DTOs;

namespace Servicios
{
    internal class ArticuloProfile : Profile
    {
        public ArticuloProfile()
        {
            CreateMap<ArticuloDTO, Articulo>();
            CreateMap<Articulo, ArticuloDTO>();
        }
    }
}