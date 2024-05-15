using Dominio.Interfaces;

namespace Dominio.Modelos
{
    public class Direccion:IValidable
    {
        public string Ciudad { get; set; }
        public string Calle { get; set; }
        public int NumeroPuerta { get; set; }

        public Direccion(string ciudad, string calle, int numeroPuerta)
        {
            Ciudad = ciudad;
            Calle = calle;
            NumeroPuerta = numeroPuerta;
        }

        public void Validar()
        {
            Utiles.ExcepcionSiStringVacio(Ciudad, "Ciudad inválida");
            Utiles.ExcepcionSiStringVacio(Calle, "Calle inválida");
            Utiles.ExcepcionSiNumeroNegativo(NumeroPuerta, "Número de puerta inválido");
        }
    }
}