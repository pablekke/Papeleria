using Dominio.Modelos;

namespace ComercioMVC.Models
{
    public class ParametrosViewModel
    {
        //pedidos
        public double IVA { get; set; } = Pedido.IVA * 100;

        //pedidos comunes
        public double Recargo { get; set; } = PedidoComun.Recargo * 100;
        public double DistanciaMaximaKm { get; set; } = PedidoComun.DistanciaMaximaKm;
        public int DiasMinimosEntrega { get; set; } = PedidoComun.DiasMinimosEntrega;

        //pedidos exprés
        public double RecargoBase { get; set; } = PedidoExpres.RecargoBase * 100;
        public double RecargoMismoDia { get; set; } = PedidoExpres.RecargoMismoDia * 100;
        public int PlazoEntregaMaximo { get; set; } = PedidoExpres.PlazoEntregaMaximo;
    }
}
