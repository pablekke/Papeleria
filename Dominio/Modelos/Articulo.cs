using Dominio.Interfaces;
using Dominio.Excepciones;

namespace Dominio.Modelos
{
    public class Articulo: IValidable, ICopiable<Articulo>
    {

        public int ArticuloId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
        public double Precio { get; set; }
        public int Stock { get; set; }

        public void Copiar(Articulo articulo)
        {
            ArticuloId = articulo.ArticuloId;
            Nombre = articulo.Nombre;
            Descripcion = articulo.Descripcion;
            Codigo = articulo.Codigo;
            Precio = articulo.Precio;
            Stock = articulo.Stock;
        }

        public void Validar()
        {
            ValidarNombre(Nombre);
            ValidarDescripcion(Descripcion);
            ValidarCodigo(Codigo);
            Utiles.ExcepcionSiNumeroNegativo(Precio, "Precio inválido");
            Utiles.ExcepcionSiNumeroNegativo(Stock, "Stock inválido");
        }
        private void ValidarNombre(string nombre)
        {
            if (nombre.Length <= 10 && nombre.Length >= 200)
            {
                throw new ExcepcionElementoInvalido("El nombre debe tener más de 10 y menos de 200 caracteres");
            }
        }
        private void ValidarDescripcion(string desc) {
            if (desc.Length < 5)
            {
                throw new ExcepcionElementoInvalido("La descripción debe tener más de 5 caracteres");
            }
        }
        private void ValidarCodigo(string codigo)
        {
            if (codigo.Length != 13)
            {
                throw new ExcepcionElementoInvalido("El código de proveedor de tiene que tener exáctamente 13 caracteres");
            }
        }

    }
}
