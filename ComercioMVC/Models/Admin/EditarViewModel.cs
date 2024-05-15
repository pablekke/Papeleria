using Dominio.DTOs;
using Dominio.Modelos;

namespace ComercioMVC.Models
{
    public class EditarUsuarioViewModel
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public bool EsAdmin { get; set; }
        public EditarUsuarioViewModel()
        {
            
        }
        public EditarUsuarioViewModel(UsuarioDTO u)
        {
            UsuarioId = u.UsuarioId;
            Nombre = u.Nombre;
            Apellido = u.Apellido;
            Email = u.Email;
            EsAdmin = u.EsAdmin;
        }
        public UsuarioDTO ToDto()
        {
            return new UsuarioDTO
            {
                UsuarioId = UsuarioId,
                Nombre = Nombre,
                Apellido = Apellido,
                Email = Email,
                EsAdmin = EsAdmin
            };
        }
    }

}
