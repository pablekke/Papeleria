using Dominio.Modelos;

namespace AccesoADatos
{
    public interface IRepositorioUsuario : IRepositorio<Usuario>
    {
        bool ExisteEmail(string email);
        Usuario? ExisteUsuario(string email, string contraseña);
        IEnumerable<Usuario> GetUsuarioPorString(string texto);
    }
}