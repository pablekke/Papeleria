using ComercioMVC.Models;
using Dominio.DTOs;
using Microsoft.AspNetCore.Mvc;
using Servicios;

namespace ComercioMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IServicioUsuario _servicioUsuario;
        public AdminController(IServicioUsuario servicioUsuario)
        {
            _servicioUsuario = servicioUsuario;
        }

        private IActionResult? chequeoUsuarioValido()
        {

            int? id = HttpContext.Session.GetInt32("Id");
            bool UsuarioEsAdmin = HttpContext.Session.GetString("EsAdmin") == "true";
            if (id == null)
            {
                return RedirectToAction("Login", "Cuenta");
            }
            // Redirigir a los usuarios administradores a su panel de control
            if (UsuarioEsAdmin)
            {
                return RedirectToAction("Usuarios", "Admin");
            }
            return null;
        }
        // GET: UsuarioController
        public ActionResult Usuarios(string n = "", string esAdmin = "")
        {
            chequeoUsuarioValido();

            // Obtener usuarios filtrados por nombre o email y rol
            IEnumerable<UsuarioDTO> usuarios = _servicioUsuario.GetUsuarioPorString(n);

            // Aplicar filtro de administradores si es seleccionado
            if (esAdmin == "true")
            {
                usuarios = usuarios.Where(u => u.EsAdmin == true);
            }

            else if (esAdmin == "false")
            {
                usuarios = usuarios.Where(u => u.EsAdmin == false);
            }

            ListarUsuariosViewModel usuariosMostrar = new ListarUsuariosViewModel()
            {
                Usuarios = usuarios
            };

            return View(usuariosMostrar);
        }

        [HttpGet]
        public ActionResult Registro()
        {
            if (HttpContext.Session.GetString("EsAdmin") != "true")
            {
                return RedirectToAction("Login", "Cuenta");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registro(RegistroViewModel form)
        {
            try
            {
                var usuarioDto = form.CrearUsuarioDTO();
                _servicioUsuario.Crear(usuarioDto);
                TempData["MensajeExito"] = "Usuario registrado correctamente.";
                return RedirectToAction("Usuarios");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View(form);
            }
        }

        // GET: Usuario/Editar/5
        [HttpGet]
        public IActionResult Editar(int id)
        {
            chequeoUsuarioValido();
            var usuario = _servicioUsuario.GetPorId(id);
            var model = new EditarUsuarioViewModel(usuario);
            return View(model);
        }

        // POST: UsuarioController/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, EditarUsuarioViewModel model) 
        {
            try
            {
                var usuarioDTO = _servicioUsuario.GetPorId(id);
                model.UsuarioId = id;
                if (UsuarioFueModificado(usuarioDTO, model))
                {
                    _servicioUsuario.Actualizar(id, usuarioDTO);
                }

                return RedirectToAction(nameof(Usuarios));
            }
            catch (Exception e)
            {
                ViewBag.Error = "Error al actualizar el usuario: " + e.Message;
                return View(model);
            }
        }

        private bool UsuarioFueModificado(UsuarioDTO usuarioDTO, EditarUsuarioViewModel usuario)
        {
            bool updated = false;
            if (usuario.Nombre != usuarioDTO.Nombre)
            {
                usuarioDTO.Nombre = usuario.Nombre;
                updated = true;
            }
            if (usuario.Apellido != usuarioDTO.Apellido)
            {
                usuarioDTO.Apellido = usuario.Apellido;
                updated = true;
            }
            if (usuario.Email != usuarioDTO.Email)
            {
                usuarioDTO.Email = usuario.Email;
                updated = true;
            }
            if (usuario.EsAdmin != usuarioDTO.EsAdmin)
            {
                usuarioDTO.EsAdmin = usuario.EsAdmin;
                updated = true;
            }
            return updated;
        }
        
        public ActionResult Borrar(int id)
        {
            chequeoUsuarioValido();
            try
            {
                _servicioUsuario.Borrar(id);
                TempData["EliminacionExitosa"] = "El usuario se ha eliminado correctamente";
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            return RedirectToAction(nameof(Usuarios));
        }
    }
}