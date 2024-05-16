using Dominio.DTOs;

namespace Servicios
{
    public interface IServicioUsuario : IServicio<UsuarioDTO>
    {
        UsuarioDTO GetPorId(int id);
        bool ExisteEmail(string email);
        UsuarioDTO? ExisteUsuario(string email, string contraseña);
        IEnumerable<UsuarioDTO> GetUsuarioPorString(string str);
    }
}