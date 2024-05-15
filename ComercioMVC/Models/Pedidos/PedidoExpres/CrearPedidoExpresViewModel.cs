//using Dominio.DTOs;
//using System.ComponentModel.DataAnnotations;

//namespace ComercioMVC.Models.Pedidos
//{
//    public class CrearPedidoExpressViewModel
//    {
//        [Required(ErrorMessage = "Debe seleccionar un cliente.")]
//        public int ClienteId { get; set; }

//        [Required]
//        public DateTime FechaEntrega { get; set; }
//        public IEnumerable<ClienteDTO> Clientes { get; set; }
//        public IEnumerable<ArticuloDTO> Articulos { get; set; }
//        [MinLength(1, ErrorMessage = "Debe agregar al menos una línea al pedido.")]
//        public List<LineaPedidoDTO> Lineas { get; set; } = new List<LineaPedidoDTO>();
//        [Range(1, int.MaxValue, ErrorMessage = "Debe agregar al menos un artículo al pedido.")]
//        public int TotalArticulos => Lineas.Count;
//        [Required]
//        public bool EntregaMismoDia { get; set; }
//    }

//}
