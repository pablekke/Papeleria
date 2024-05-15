using Dominio.DTOs;

namespace WebAPI
{
    public class Pedidos
    {
        public DateTime FechaPrometidaEntrega { get; set; }
        public ClienteDTO Cliente { get; set; }
        public double PrecioTotal { get; set; }
    }
}
