namespace Dominio.DTOs
{
    public class UsuarioDTO
    {
        public int UsuarioId { get; set; }
        public bool EsAdmin { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public string ContraseñaHasheada { get; set; }
    }
}
