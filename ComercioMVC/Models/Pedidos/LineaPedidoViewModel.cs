using Dominio.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ComercioMVC.Models.Pedidos
{
    public class LineaPedidoViewModel
    {
        public int LineaPedidoId { get; set; }
        public int PedidoId { get; set; }
        public int ArticuloId { get; set; }
        public ArticuloDTO Articulo { get; set; }
        public int Cantidad { get; set; }
        public double PrecioUnitario { get; set; }

        //public LineaPedidoViewModel()
        //{
        //    LineaPedidoId = LineaPedidoId;
        //    PedidoId = PedidoId;
        //    ArticuloId = ArticuloId;
        //    Articulo = Articulo;
        //    Cantidad = Cantidad;
        //    PrecioUnitario = PrecioUnitario;
        //}

        public LineaPedidoDTO CrearLineaDTO()
        {
            var dto = new LineaPedidoDTO
            {
                LineaPedidoId = LineaPedidoId,
                PedidoId = PedidoId,
                ArticuloId = ArticuloId,
                Articulo = Articulo,
                Cantidad = Cantidad,
                PrecioUnitario = PrecioUnitario
            };
            return dto;
        }
    }
}
