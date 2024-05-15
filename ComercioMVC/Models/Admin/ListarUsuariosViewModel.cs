using Dominio.DTOs;

namespace ComercioMVC.Models
{
    public class ListarUsuariosViewModel
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set;}
        public string Apellido { get; set;}
        public string Email { get; set;}
        public bool EsAdmin { get; set;}

        public IEnumerable<UsuarioDTO> Usuarios { get; set; }
    }
}
