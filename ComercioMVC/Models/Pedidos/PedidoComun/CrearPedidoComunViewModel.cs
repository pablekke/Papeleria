using Dominio.DTOs;
using Dominio.Modelos;
using System.ComponentModel.DataAnnotations;

namespace ComercioMVC.Models
{
    public class PedidoComunViewModel
    {
        public int PedidoId { get; set; }
        public int ClienteId { get; set; }
        public int ArticuloId { get; set; }
        public ClienteDTO Cliente { get; set; }

        [Required(ErrorMessage = "La fecha de entrega es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaEntregaPrometida { get; set; } = DateTime.Now.Date.AddDays(PedidoComun.DiasMinimosEntrega + 1);
        public List<LineaPedidoDTO> Lineas { get; set; } = new List<LineaPedidoDTO>();

        public IEnumerable<ClienteDTO> Clientes { get; set; }
        public IEnumerable<ArticuloDTO> Articulos { get; set; }
        public double Total { get; set; }
        public int Cantidad { get; set; }
        public double Recargo { get; set; }

        public PedidoComunDTO CrearDTO()
        {
            var dto = new PedidoComunDTO
            {
                PedidoId = PedidoId,
                ClienteId = ClienteId,
                Cliente = Cliente,
                FechaEntregaPrometida = FechaEntregaPrometida,
                Lineas = Lineas,
                Total = Total
            };

            return dto;
        }
    }
}