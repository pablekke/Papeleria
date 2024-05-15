using Dominio.Interfaces;

namespace Dominio.DTOs
{
    public class UsuarioDTOMostrar
    {
        public int UsuarioId { get; }
        public bool EsAdmin { get; }
        public string Nombre { get; }
        public string Apellido { get; }
        public string Email { get; }
        public string ContraseñaHasheada { get; }
    }
}
