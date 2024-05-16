using Dominio.DTOs;
using Dominio.Modelos;
using System.ComponentModel.DataAnnotations;

namespace ComercioMVC.Models
{
    public class PedidoExpresViewModel
    {
        public int PedidoId { get; set; }
        public int ClienteId { get; set; }
        public int ArticuloId { get; set; }
        public ClienteDTO Cliente { get; set; }

        [Required(ErrorMessage = "La fecha de entrega es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaEntregaPrometida { get; set; } = DateTime.Now.Date.AddDays(1);
        public DateTime FechaEntregaMaxima { get; set; } = DateTime.Now.Date.AddDays(PedidoExpres.PlazoEntregaMaximo);

        public DateTime FechaHoy = DateTime.Now.Date;
        public List<LineaPedidoDTO> Lineas { get; set; } = new List<LineaPedidoDTO>();

        public IEnumerable<ClienteDTO> Clientes { get; set; }
        public IEnumerable<ArticuloDTO> Articulos { get; set; }
        public double Total { get; set; }
        public int Cantidad { get; set; }
        public double Recargo { get; set; }
        public bool EntregaMismoDia { get; set; } = false;

        public PedidoExpresDTO CrearDTO()
        {
            var dto = new PedidoExpresDTO
            {
                PedidoId = PedidoId,
                ClienteId = ClienteId,
                Cliente = Cliente,
                FechaEntregaPrometida = FechaEntregaPrometida,
                Lineas = Lineas,
                Total = Total,
                EntregaMismoDia = EntregaMismoDia
            };
            return dto;
        }
    }
}