using Dominio.DTOs;
using System.ComponentModel.DataAnnotations;
namespace ComercioMVC.Models
{
    public class RegistroViewModel
    {
        public bool EsAdmin { get; set; }
        public int UsuarioId { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe proporcionar una dirección de email válida.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[.,;!])[A-Za-z\d.,;!]{6,}$")]
        public string Contraseña { get; set; }

        [DataType(DataType.Password)]
        [Compare("Contraseña", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmarContraseña { get; set; }

        public RegistroViewModel() { }

        public RegistroViewModel(UsuarioDTO usuarioDTO)
        {
            Nombre = usuarioDTO.Nombre;
            Apellido = usuarioDTO.Apellido;
            Email = usuarioDTO.Email;
            EsAdmin = usuarioDTO.EsAdmin;
        }

        public UsuarioDTO CrearUsuarioDTO()
        {
            return new UsuarioDTO()
            {
                UsuarioId = UsuarioId,
                EsAdmin = EsAdmin,
                Nombre = Nombre,
                Apellido = Apellido,
                Email = Email,
                Contraseña = Contraseña,
                ContraseñaHasheada = HashearContraseña(Contraseña)
            };
        }
        private string HashearContraseña(string contraseña)
        {
            return BCrypt.Net.BCrypt.HashPassword(contraseña);
        }
    }
}
