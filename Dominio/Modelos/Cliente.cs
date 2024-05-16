using Dominio.Excepciones;
using Dominio.Interfaces;

namespace Dominio.Modelos
{
    public class Cliente : IValidable, ICopiable<Cliente>
    {
        public int ClienteId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string RazonSocial { get; set; }
        public string RUT { get; set; }
        public Direccion Direccion { get; set; }
        public double Distancia { get; set; }

        public void Validar()
        {
            Utiles.ExcepcionSiNumeroNegativo(ClienteId, "El identificador es inválido");
            Utiles.ExcepcionSiStringVacio(RazonSocial, "Razón social inválida");
            Utiles.ExcepcionSiNumeroNegativo(Distancia, "Distancia inválida");
            Utiles.ExcepcionSiStringVacio(Nombre, "Nombre vacío");
            Utiles.ExcepcionSiStringVacio(Apellido, "Apellido vacío");
            Utiles.ExcepcionSiEmailInvalido(Email, "Email invalido");
            ValidarRUT(RUT);
        }

        public void Copiar(Cliente cliente)
        {
            ClienteId = cliente.ClienteId;
            Nombre = cliente.Nombre;
            Apellido = cliente.Apellido;
            Email = cliente.Email;
            RazonSocial = cliente.RazonSocial;
            Direccion = cliente.Direccion;
            Distancia = cliente.Distancia;
        }

        private void ValidarRUT(string rut) {
            if (rut == null || rut.Length != 12)
            {
                throw new ExcepcionElementoInvalido("El RUT es inválido, debe tener exactamente 12 caracteres");
            }
        }
    }
}