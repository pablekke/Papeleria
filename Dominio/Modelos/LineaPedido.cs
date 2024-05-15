using Dominio.Interfaces;

namespace Dominio.Modelos
{
    public class LineaPedido:IValidable, ICopiable<LineaPedido>
    {
        public int LineaPedidoId { get; set; }
        public int PedidoId { get; set; }
        public int ArticuloId { get; set; }
        public Articulo Articulo { get; set; }
        public int Cantidad { get; set; }
        public double PrecioUnitario { get; set; }

        public double Subtotal()
        {
            return Cantidad * PrecioUnitario;
        }

        public void Copiar(LineaPedido lp)
        {
            LineaPedidoId = lp.LineaPedidoId;
            PedidoId = lp.PedidoId;
            ArticuloId = lp.ArticuloId;
            Cantidad = lp.Cantidad;
            PrecioUnitario = lp.PrecioUnitario;
        }

        public void Validar()
        {
            Utiles.ExcepcionSiNumeroNegativo(Cantidad, "Cantidad inválida");
            Utiles.ExcepcionSiNumeroNegativo(PrecioUnitario, "Precio inválido");
        }
    }
}
