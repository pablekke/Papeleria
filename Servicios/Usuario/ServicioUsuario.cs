using AccesoADatos;
using AutoMapper;
using Dominio.DTOs;
using Dominio.Modelos;

namespace Servicios
{
    public class ServicioUsuario : Servicio<Usuario, UsuarioDTO>, IServicioUsuario
    {
        private readonly IRepositorioUsuario _repositorio;
        public ServicioUsuario(IMapper mapeador, IRepositorioUsuario repositorio) : base(mapeador, repositorio)
        {
            _repositorio = repositorio;
        }

        public bool ExisteEmail(string email)
        {
            return _repositorio.ExisteEmail(email);
        }

        public UsuarioDTO? ExisteUsuario(string email, string contraseña)
        {
            var usuario = _repositorio.ExisteUsuario(email, contraseña);
            return _mapeador.Map<UsuarioDTO>(usuario);
        }

        public UsuarioDTO GetPorId(int id)
        {
            ExcepcionSiIdInvalido(id);
            var usuario = _repositorio.GetPorId(id);
            ExcepcionSiObjetoNoExiste(usuario);
            return _mapeador.Map<UsuarioDTO>(usuario);
        }

        public IEnumerable<UsuarioDTO> GetUsuarioPorString(string texto)
        {
            var usuarios = _repositorio.GetUsuarioPorString(texto);
            return _mapeador.Map<IEnumerable<UsuarioDTO>>(usuarios);
        }

        #region Excepciones y controles
        protected override void ControlesAntesDeCrear(UsuarioDTO usuarioDto)
        {
            if (ExisteEmail(usuarioDto.Email))
            {
                throw new Exception("El email ya está en uso.");
            }
        }
        protected override void ControlesAntesDeActualizar(int id, UsuarioDTO dto)
        {
            //si el nuevo email es distinto del actual chequea que no lo tenga otro usuario ya
            var modelo = GetPorId(id);
            if (modelo.Email != dto.Email)
            {
                ExcepcionSiExisteEmail(dto.Email);
            }
        }
        private void ExcepcionSiExisteEmail(string email)
        {
            if (ExisteEmail(email))
            {
                throw new Exception("El email ya está en uso.");
            }
        }
        #endregion
    }
}
