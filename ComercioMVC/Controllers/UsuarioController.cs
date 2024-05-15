using ComercioMVC.Models;
using Dominio.DTOs;
using Dominio.Modelos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Servicios;

namespace ComercioMVC.Controllers
{
    public class UsuarioController : Controller
    {
        #region Constructor
        private readonly IServicioCliente _servicioCliente;
        private readonly IServicioArticulo _servicioArticulo;
        private readonly IServicioPedido _servicioPedido;
        private readonly IServicioPedidoExpres _servicioPedidoExpres;
        private readonly IServicioPedidoComun _servicioPedidoComun;
        private readonly IHttpClientFactory _httpClientFactory;

        public UsuarioController(
            IHttpClientFactory httpClientFactory,
            IServicioCliente servicioCliente,
            IServicioArticulo servicioArticulo,
            IServicioPedido servicioPedido,
            IServicioPedidoExpres servicioPedidoExpres,
            IServicioPedidoComun servicioPedidoComun)
        {
            _httpClientFactory = httpClientFactory;
            _servicioCliente = servicioCliente;
            _servicioPedido = servicioPedido;
            _servicioArticulo = servicioArticulo;
            _servicioPedidoExpres = servicioPedidoExpres;
            _servicioPedidoComun = servicioPedidoComun;
        }
        private IActionResult? chequeoUsuarioValido() { 
        
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
        #endregion

        #region Clientes

        //mostrar clientes
        public IActionResult Clientes(string nombre = "", double monto = 0)
        {
            chequeoUsuarioValido();
            IEnumerable<ClienteDTO> clientes;
            if (monto != 0 && nombre == "")
            {
                clientes = _servicioCliente.GetClientesPorMonto(monto);
            }
            else if (monto == 0)
            {

                clientes = _servicioCliente.GetClientesPorString(nombre);
            }
            else
            {
                clientes = _servicioCliente.GetClientesPorStringYMonto(nombre, monto);
            }
            ListarClientesViewModel clientesMostrar = new ListarClientesViewModel()
            {
                Clientes = clientes
            };
            return View(clientesMostrar);
        }

        //crear cliente
        
        public IActionResult RegistroCliente()
        {
            chequeoUsuarioValido();
            return View();
        }

        [HttpPost]
        public IActionResult RegistroCliente(RegistroClienteViewModel formulario)
        {
            try
            {
                var cliente = formulario.CrearClienteDTO();
                _servicioCliente.Crear(cliente);
                return RedirectToAction("Clientes");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(formulario);
        }
        #endregion

        #region Articulos

        #region ArticulosPorAPI
        //private async Task<string> LlammarAPI(string url)
        //{
        //    HttpClient httpClient = _httpClientFactory.CreateClient();
        //    try
        //    {
        //        using (HttpResponseMessage response = await httpClient.GetAsync(url))
        //        {
        //            if (response.IsSuccessStatusCode)
        //            {
        //                var content = await response.Content.ReadAsStringAsync();
        //                return content;
        //            }
        //            else
        //            {
        //                throw new Exception($"Error al obtener los artículos: {response.StatusCode}");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = ($"Error al realizar la petición: {ex.Message}");
        //        throw;
        //    }
        //}
        //private async Task<List<ArticuloDTO>> GetArticulos(string nombre, double monto)
        //{
        //    var url = $"http://localhost:5267/Articulos?nombre={nombre}&monto={monto}";
        //    string articulosJson = await LlammarAPI(url);
        //    return JsonConvert.DeserializeObject<List<ArticuloDTO>>(articulosJson) ?? new List<ArticuloDTO>();
        //}
        //private async Task<ArticuloDTO> GetArticulo(int id)
        //{
        //    var url = $"http://localhost:5267/Articulos?id={id}";
        //    string articulo = await LlammarAPI(url);
        //    return JsonConvert.DeserializeObject<ArticuloDTO>(articulo);
        //}
        #endregion

        [HttpGet]
        public IActionResult Articulos(string nombre = "", double monto = 0)
        {
            chequeoUsuarioValido();
            return View(_servicioArticulo.GetArticulosFiltrados(nombre, monto));
        }

        public IActionResult RegistroArticulo()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RegistroArticulo(RegistroArticuloViewModel formulario)
        {
            try
            {
                var articulo = formulario.CrearArticuloDTO();
                _servicioArticulo.Crear(articulo);
                return RedirectToAction("Articulos");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(formulario);
        }

        #endregion
    }
}
