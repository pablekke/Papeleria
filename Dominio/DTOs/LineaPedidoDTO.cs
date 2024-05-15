namespace Dominio.DTOs
{
    public class LineaPedidoDTO
    {
        public int LineaPedidoId { get; set; }
        public int PedidoId { get; set; }
        public int ArticuloId { get; set; }
        public ArticuloDTO Articulo { get; set; }
        public int Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
        public double Subtotal()
        {
            return Cantidad * PrecioUnitario;
        }
    }
}