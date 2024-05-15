using AutoMapper;
using Dominio.Modelos;
using Dominio.DTOs;

namespace Servicios
{
    internal class UsuarioProfile: Profile
    {
        public UsuarioProfile()
        {
            CreateMap<UsuarioDTO, Usuario>();
            CreateMap<Usuario, UsuarioDTO>();
        }
    }
}
