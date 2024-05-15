using AutoMapper;
using Dominio.Modelos;
using Dominio.DTOs;

namespace Servicios
{
    internal class UsuarioMostrarProfile: Profile
    {
        public UsuarioMostrarProfile()
        {
            CreateMap<Usuario, UsuarioDTOMostrar>();
            CreateMap<UsuarioDTOMostrar, Usuario>();
        }
    }
}
