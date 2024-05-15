using Dominio.Excepciones;
using Dominio.Interfaces;

namespace Dominio.Modelos
{
    public class PedidoExpres : Pedido, ICopiable<PedidoExpres>
    {
        public bool EntregaMismoDia { get; set; }
        public static double RecargoBase { get; set; } = 0.10;
        public static double RecargoMismoDia { get; set; } = 0.15;
        public static int PlazoEntregaMaximo { get; set; } = 5;
        
        #region Métodos
        public static double CalcularRecargo(double subtotal, bool seEntregaElMismoDia)
        {
            var ret = subtotal;
            if (seEntregaElMismoDia)
                ret *= PedidoExpres.RecargoMismoDia;
            else
                ret *= PedidoExpres.RecargoBase;
            return Math.Round(ret, 2);
        }

        public static double CalcularTotal(double subtotal, bool seEntregaElMismoDia)
        {
            return TotalMasIVASinRecargo(subtotal) + CalcularRecargo(subtotal, seEntregaElMismoDia);
        }

        public override void ValidarFechaEntregaPrometida(DateTime fecha) {
            int diasDiferencia = (fecha.Date - DateTime.Now.Date).Days;

            // Verifica si la fecha prometida es pasada
            if (DateTime.Now.Date > fecha.Date && !EntregaMismoDia)
            {
                throw new ExcepcionElementoInvalido("Fecha inválida: La fecha de entrega no puede ser una fecha pasada.");
            }

            // Verifica si la fecha prometida es hoy
            if (DateTime.Now.Date == fecha.Date && !EntregaMismoDia)
            {
                throw new ExcepcionElementoInvalido("Fecha inválida: La fecha de entrega no puede ser hoy");
            }

            // Verifica que la fecha de entrega no exceda el plazo máximo de entrega establecido
            if (diasDiferencia > PlazoEntregaMaximo)
            {
                throw new ExcepcionElementoInvalido($"Fecha inválida: La fecha de entrega no debe exceder los {PlazoEntregaMaximo} días desde hoy.");
            }
        }

        public static void ActualizarRecargoBase(double recargo)
        {
            Utiles.ExcepcionPorcentajeInvalido(recargo);
            RecargoBase = recargo;
        }

        public static void ActualizarRecargoMismoDia(double recargo)
        {
            Utiles.ExcepcionPorcentajeInvalido(recargo);
            RecargoMismoDia = recargo;
        }

        public static void ActualizarPlazoEntregaMaximo(int dias)
        {
            Utiles.ExcepcionSiNumeroNegativo(dias, "Plazo inválido");
            PlazoEntregaMaximo = dias;
        }

        public void Copiar(PedidoExpres pedido)
        {
            EntregaMismoDia = pedido.EntregaMismoDia;
            base.Copiar(pedido);
            Total = pedido.Total;
        }
        #endregion
    }
}
