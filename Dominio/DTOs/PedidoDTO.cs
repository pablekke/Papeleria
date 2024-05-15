namespace Dominio.DTOs
{
    public class PedidoDTO
    {
        public int PedidoId { get; set; }
        public int ClienteId { get; set; }
        public ClienteDTO Cliente { get; set; } 
        public DateTime FechaEntregaPrometida { get; set; }
        public DateTime FechaEmision { get; set; } = DateTime.Now;
        public List<LineaPedidoDTO> Lineas { get; set; } = new List<LineaPedidoDTO>();
        public bool Entregado { get; set; }
        public double Total { get; set; }
        public bool Anulado { get; set; }
    }
}