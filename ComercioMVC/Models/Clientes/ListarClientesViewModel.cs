using Dominio.DTOs;

namespace ComercioMVC.Models
{
    public class ListarClientesViewModel
    {
        public int ClienteId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string RazonSocial { get; set; }
        public string RUT { get; set; }
        public string Direccion { get; set; }
        public double Distancia { get; set; }

        public IEnumerable<ClienteDTO> Clientes { get; set; }
    }
}
