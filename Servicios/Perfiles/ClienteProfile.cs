using AutoMapper;
using Dominio.Modelos;
using Dominio.DTOs;

public class ClienteProfile : Profile
{
    public ClienteProfile()
    {
        // Mapeo de Cliente a ClienteDTO y de ClienteDTO a Cliente
        CreateMap<Cliente, ClienteDTO>();
        CreateMap<ClienteDTO, Cliente>();

        // Mapeo de Direccion a DireccionDTO y de DireccionDTO a Direccion
        CreateMap<Direccion, DireccionDTO>();
        CreateMap<DireccionDTO, Direccion>();
    }
}
