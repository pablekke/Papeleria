using AutoMapper;
using Dominio.Modelos;
using Dominio.DTOs;

public class ClienteProfile : Profile
{
    public ClienteProfile()
    {
        CreateMap<Cliente, ClienteDTO>();
        CreateMap<ClienteDTO, Cliente>();

        CreateMap<Direccion, DireccionDTO>();
        CreateMap<DireccionDTO, Direccion>();
    }
}