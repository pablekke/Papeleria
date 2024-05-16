using Dominio.DTOs;

namespace ComercioMVC.Models
{
    public class PedidosViewModel
    {
        public int PedidoId { get; set; }
        public IEnumerable<PedidoDTO> Pedidos { get; set; } = new List<PedidoDTO>();
        public DateTime? FechaIngresada { get; set; } = null;
    }
}