using Dominio.Excepciones;
using System.Text.RegularExpressions;
namespace Dominio
{
    internal class Utiles
    {
        public static void ExcepcionSiElementoNulo(Object o, string message)
        {
            if (o == null)
            {
                throw new ExcepcionElementoInvalido(message);
            }
        }
        public static void ExcepcionSiStringVacio(string s, string message)
        {
            if (String.IsNullOrEmpty(s))
            {
                throw new ExcepcionElementoInvalido(message);
            }
        }
        public static void ExcepcionSiNumeroNegativo(int number, string message)
        {
            if (number < 0)
            {
                throw new ExcepcionElementoInvalido(message);
            }
        }
        public static void ExcepcionSiNumeroNegativo(double number, string message)
        {
            if (number < 0)
            {
                throw new ExcepcionElementoInvalido(message);
            }
        }
        public static void ExcepcionSiCero(int number, string message)
        {
            if (number == 0)
            {
                throw new ExcepcionElementoInvalido(message);
            }
        }
        public static void ExcepcionSiFechaFutura(DateTime dateTime, string message)
        {
            if (dateTime > DateTime.Now)
            {
                throw new ExcepcionElementoInvalido(message);
            }
        }
        public static void ExcepcionSiEmailInvalido(string email, string message)
        {
            if (string.IsNullOrEmpty(email) || !EsEmailValido(email))
            {
                throw new ExcepcionElementoInvalido(message);
            }
        }
        private static bool EsEmailValido(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
        public static void ExcepcionSiListaVacia<T>(IEnumerable<T> lista, string mensaje)
        {
            if (!lista.Any())
            {
                throw new ExcepcionElementoInvalido(mensaje);
            }
        }
        public static void ExcepcionPorcentajeInvalido(double porc)
        {
            porc /= 100;
            if (porc < 0 || porc > 1)
            {
                throw new ExcepcionElementoInvalido("Porcentaje inválido");
            }
        }
    }
}
