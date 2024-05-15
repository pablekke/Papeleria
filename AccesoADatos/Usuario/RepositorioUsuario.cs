using Dominio.Modelos;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace AccesoADatos
{
    public class RepositorioUsuario : Repositorio<Usuario>, IRepositorioUsuario
    {
        public RepositorioUsuario(DbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public bool ExisteEmail(string email)
        {
            return _contexto.Set<Usuario>().Any(u => u.Email == email);
        }
        public Usuario? ExisteUsuario(string email, string contraseña)
        {
            var usuario = _contexto.Set<Usuario>().FirstOrDefault(u => u.Email == email);
            if (usuario != null && contraseñaValida(contraseña, usuario.ContraseñaHasheada))
            {
                return usuario;
            }
            return null;
        }
        private bool contraseñaValida(string cLocal, string cBase) {
            return BCrypt.Net.BCrypt.Verify(cLocal, cBase);
        }

        public IEnumerable<Usuario> GetUsuarioPorString(string texto)
        {
            return _contexto.Set<Usuario>().
                Where(u => u.Nombre.ToLower().Contains(texto.ToLower()) ||
                           u.Email.ToLower().Contains(texto.ToLower()) ||
                           u.Apellido.ToLower().Contains(texto.ToLower()));
        }
    }
}
