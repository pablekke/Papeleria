using Dominio.Interfaces;

namespace Dominio.Modelos
{
    public abstract class Pedido : IValidable, ICopiable<Pedido>
    {
        public static double IVA { get; set; } = 0.22;
        public int PedidoId { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime FechaEmision { get; private set; }
        public DateTime FechaEntregaPrometida { get; set; }
        public List<LineaPedido> Lineas { get; set; }
        public double Total { get; set; }
        public bool Entregado { get; set; } = false;
        public bool Anulado { get; set; } = false;

        #region Metodos
        public static void ActualizarIVA(double iva)
        {
            Utiles.ExcepcionPorcentajeInvalido(iva);
            IVA = iva;
        }
        public abstract void ValidarFechaEntregaPrometida(DateTime fecha);
        public static double TotalMasIVASinRecargo(double total)
        {
            return Math.Round(total * (1 + IVA), 2);
        }

        public virtual void Validar()
        {
            Utiles.ExcepcionSiFechaFutura(FechaEmision, "Fecha incorrecta");
            ValidarFechaEntregaPrometida(FechaEntregaPrometida);
        }

        public void Copiar(Pedido pedido)
        {
            PedidoId = pedido.PedidoId;
            ClienteId = pedido.ClienteId;
            Cliente = pedido.Cliente;
            FechaEmision = DateTime.Now;
            FechaEntregaPrometida = pedido.FechaEntregaPrometida;
            Lineas = new List<LineaPedido>(pedido.Lineas);
            Entregado = false;
            Anulado = false;
        }
        #endregion
    }
}
