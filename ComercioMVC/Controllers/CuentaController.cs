using ComercioMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios;

namespace ComercioMVC.Controllers
{
    public class CuentaController : Controller
    {
        private readonly IServicioUsuario _servicioUsuario;

        public CuentaController(IServicioUsuario servicioUsuario)
        {
            _servicioUsuario = servicioUsuario;
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            int? id = HttpContext.Session.GetInt32("Id");
            bool UsuarioEsAdmin = HttpContext.Session.GetString("EsAdmin") == "true";
            if (id != null)
            {
                if (UsuarioEsAdmin)
                {
                    return RedirectToAction("Usuarios", "Admin");
                }
                return RedirectToAction("Clientes", "Usuario");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var usuario = _servicioUsuario.ExisteUsuario(model.Email, model.Contraseña);

            if (usuario != null && usuario.Email != null)
            {
                HttpContext.Session.SetInt32("Id", usuario.UsuarioId);
                HttpContext.Session.SetString("EsAdmin", usuario.EsAdmin ? "true" : "false");
                if (usuario.EsAdmin)
                {
                    return RedirectToAction("Usuarios", "Admin");
                }
                return RedirectToAction("Clientes", "Usuario");
            }

            ViewBag.Error = "Credenciales inválidas";
            return View(model);
        }
        
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LogIn");
        }
    }
}
