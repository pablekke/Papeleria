using Dominio.Excepciones;
using Dominio.Interfaces;

namespace Dominio.Modelos
{
	public class PedidoComun : Pedido, ICopiable<PedidoComun>, IValidable
	{
		public static double Recargo { get; set; } = 0.05; // Recargo por distancia
		public static double DistanciaMaximaKm { get; set; } = 100;
		public static int DiasMinimosEntrega { get; set; } = 7;
       
		#region Métodos
        public static double CalcularRecargo(double subtotal, double distancia)
		{
			if (distancia > PedidoComun.DistanciaMaximaKm)
			{
				var total = subtotal * Recargo;
				return Math.Round(total, 2);
			}
			return 0;
		}
		public static double CalcularTotal(double totalSinIVA, double distancia)
        {
            return TotalMasIVASinRecargo(totalSinIVA) + CalcularRecargo(totalSinIVA, distancia);
        }
		
		public override void ValidarFechaEntregaPrometida(DateTime fecha) {
            int diasDiferencia = (fecha.Date - DateTime.Now.Date).Days;
            // Verifica si la fecha prometida es hoy o en el pasado, o si es menor en días a la cantidad mínima de 7 días futuros
            if (DateTime.Now.Date >= fecha.Date || diasDiferencia <= DiasMinimosEntrega)
            {
                throw new ExcepcionElementoInvalido($"Fecha inválida: La fecha de entrega debe ser al menos {DiasMinimosEntrega} días después de hoy y no puede ser hoy o una fecha pasada.");
            }
        }

        public static void ActualizarRecargo(double recargo)
		{
			Utiles.ExcepcionPorcentajeInvalido(recargo);
			Recargo = recargo;
		}

		public static void ActualizarDistanciaMaximaKm(double distancia)
		{
			Utiles.ExcepcionSiNumeroNegativo(distancia, "Distancia inválida");
			DistanciaMaximaKm = distancia;
		}

		public static void ActualizarDiasMinimosEntrega(int dias)
		{
			Utiles.ExcepcionSiNumeroNegativo(dias, "Cantidad de días inválida");
			DiasMinimosEntrega = dias;
		}

		public void Copiar(PedidoComun pedido)
		{
			base.Copiar(pedido);
        }
        #endregion
    }
}
