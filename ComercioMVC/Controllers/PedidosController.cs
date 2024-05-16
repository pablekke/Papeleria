using ComercioMVC.Models;
using Dominio.DTOs;
using Dominio.Modelos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Servicios;

namespace ComercioMVC.Controllers
{
    public class PedidosController : Controller
    {
        #region Constructor
        private readonly IServicioCliente _servicioCliente;
        private readonly IServicioArticulo _servicioArticulo;
        private readonly IServicioPedido _servicioPedido;
        private readonly IServicioPedidoExpres _servicioPedidoExpres;
        private readonly IServicioPedidoComun _servicioPedidoComun;

        public PedidosController(
            IServicioCliente servicioCliente,
            IServicioArticulo servicioArticulo,
            IServicioPedido servicioPedido,
            IServicioPedidoExpres servicioPedidoExpres,
            IServicioPedidoComun servicioPedidoComun)
        {
           
            _servicioCliente = servicioCliente;
            _servicioPedido = servicioPedido;
            _servicioArticulo = servicioArticulo;
            _servicioPedidoExpres = servicioPedidoExpres;
            _servicioPedidoComun = servicioPedidoComun;
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
        #endregion

        #region Pedidos pendientes
        public IActionResult Pendientes(PedidosViewModel modelo)
        {
            chequeoUsuarioValido();

            var pedidos = _servicioPedido.GetPedidosNoEntregadosPorFecha(modelo.FechaIngresada);

            var viewModel = new PedidosViewModel
            {
                Pedidos = pedidos,
                FechaIngresada = modelo.FechaIngresada
            };

            return View(viewModel);
        }

        public IActionResult AnularPedido(int id)
        {
            chequeoUsuarioValido();

            _servicioPedido.AnularPedido(id);

            return RedirectToAction(nameof(Pendientes));
        }

        #endregion

        #region Pedidos anulados
        public IActionResult Anulados()
        {
            chequeoUsuarioValido();
            PedidosViewModel clientesMostrar = new PedidosViewModel()
            {
                Pedidos = _servicioPedido.GetPedidosAnulados()
            };
            return View(clientesMostrar);
        }
        #endregion

        #region Pedido común

        #region Funciones para pedido común

        #region Pedidos en sesión
        //lo guardo como string lo devuelvo como modelo
        private PedidoComunViewModel GetPedidoComunDeSesion()
        {
            var objetoSerializado = HttpContext.Session.GetString("NuevoPedido") as string;
            if (objetoSerializado != null)
            {
                PedidoComunViewModel modelo = JsonConvert.DeserializeObject<PedidoComunViewModel>(objetoSerializado);

                return modelo; // Enviando el objeto como ViewModel a la vista
            }
            return null;
        }
        private void SetPedidoComunEnSesion(PedidoComunViewModel modelo)
        {
            string objetoSerializado = JsonConvert.SerializeObject(modelo);
            HttpContext.Session.SetString("NuevoPedido", objetoSerializado);
        }
        #endregion

        private void AñadirLinea(PedidoComunViewModel modelo, PedidoComunViewModel modeloSesion, ArticuloDTO articulo)
        {
            //actualiza el stock
            LineaPedidoDTO? articuloEnLinea = modeloSesion.Lineas.FirstOrDefault(l => l.ArticuloId == articulo.ArticuloId);

            if (articuloEnLinea != null)
            {
                int cantidadNueva = articuloEnLinea.Cantidad + modelo.Cantidad;

                if (articulo.Stock > cantidadNueva)
                {
                    articuloEnLinea.Cantidad = cantidadNueva; // Actualiza la cantidad si no supera el stock
                }
                else if (articulo.Stock - articuloEnLinea.Cantidad == 0)
                {
                    ViewBag.ErrorLinea = $"Hay {articulo.Stock} en stock, has llegado al máximo.";
                }
                else
                {
                    int stockRestante = articulo.Stock - articuloEnLinea.Cantidad;
                    ViewBag.ErrorLinea = $"No se puede añadir la cantidad deseada. Hay {articulo.Stock} en stock, solo podés añadir {stockRestante} más.";
                }
            }
            //sino comprueba que haya stock y crea nueva linea
            else
            {
                if (modelo.Cantidad <= articulo.Stock)
                {
                    modeloSesion.Lineas.Add(new LineaPedidoDTO
                    {
                        ArticuloId = modelo.ArticuloId,
                        Cantidad = modelo.Cantidad,
                        Articulo = articulo,
                        PrecioUnitario = articulo.Precio
                    });
                }
                else
                {
                    ViewBag.ErrorLinea = $"No hay suficiente stock para el artículo. Stock disponible: {articulo.Stock}.";
                }
            }
        }
        private void RemoverLinea(int? indice, PedidoComunViewModel modeloSesion)
        {
            if (indice.HasValue && indice.Value >= 0 && indice.Value < modeloSesion.Lineas.Count)
            {
                modeloSesion.Lineas.RemoveAt(indice.Value);
            }
        }
        private void ActualizarInformacionEnSesion(PedidoComunViewModel modelo) {
            double total = modelo.Lineas.Sum(l => l.Articulo.Precio * l.Cantidad);
            double recargo = PedidoComun.CalcularRecargo(total, modelo.Cliente.Distancia);
            modelo.Total = Math.Round(total, 2);
            modelo.Recargo = Math.Round(recargo, 2);
            SetPedidoComunEnSesion(modelo);
        }
        #endregion

        [HttpGet]
        public IActionResult CrearPedidoComun()
        {
            chequeoUsuarioValido();
            var modelo = GetPedidoComunDeSesion();
            if (modelo == null) { 
                modelo = new PedidoComunViewModel
                {
                    Clientes = _servicioCliente.GetClientesPorString(""),
                    Articulos = _servicioArticulo.GetArticulosFiltrados("", 0)
                };
                SetPedidoComunEnSesion(modelo);
            }
            else if (modelo.Lineas.Count > 0){ 
                ActualizarInformacionEnSesion(modelo);
            }
            if (modelo.DiasMinimos != PedidoComun.DiasMinimosEntrega)
            {
                modelo.DiasMinimos = PedidoComun.DiasMinimosEntrega;
            }
            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CrearPedidoComun(PedidoComunViewModel modelo, int? indiceLinea, string accion)
        {
            var modeloSesion = GetPedidoComunDeSesion();

            var articulo = _servicioArticulo.GetArticuloPorId(modelo.ArticuloId);
            var cliente = _servicioCliente.GetClientePorId(modelo.ClienteId);
            
           if (cliente != null)
            {
                modeloSesion.Cliente = cliente;
                modeloSesion.ClienteId = modelo.ClienteId;

                switch (accion)
                {
                    case "AñadirLinea":
                        if (articulo != null)
                            AñadirLinea(modelo, modeloSesion, articulo);
                        else
                            ViewBag.ErrorLinea = "Debes seleccionar un artículo";
                        break;
                    case "RemoverLinea":
                        RemoverLinea(indiceLinea, modeloSesion);
                        break;

                    case "CrearPedido":
                        if (modeloSesion.Lineas.Count == 0)
                        {
                            ViewBag.ErrorPedido = "Debes agregar al menos una línea.";
                        }
                        else
                        {
                            try
                            {
                                //elimino el cliente y los articulos despues de calcular el precio total
                                var modeloDTO = modeloSesion.CrearDTO();
                                modeloDTO.Total += modeloSesion.Recargo;
                                modeloDTO.Cliente = null;
                                foreach (var linea in modeloDTO.Lineas)
                                {
                                    linea.Articulo = null;
                                }
                                _servicioPedidoComun.Crear(modeloDTO);
                                HttpContext.Session.Remove("NuevoPedido");
                                return RedirectToAction("Clientes", "Usuario");
                            }
                            catch (Exception ex)
                            {
                                ViewBag.ErrorPedido = ex.Message;
                                return View(modeloSesion);
                            }
                        }
                        break;
                }
                ActualizarInformacionEnSesion(modeloSesion);
                SetPedidoComunEnSesion(modeloSesion);
            }
            else
            {
                ViewBag.ErrorLinea = "Debes seleccionar un cliente.";
            }
            return View(modeloSesion);
        }

        #endregion

        #region Pedido exprés
        #region Funciones para pedido común

        #region Pedidos en sesión
        //lo guardo como string lo devuelvo como modelo
        private PedidoExpresViewModel GetPedidoExpresDeSesion()
        {
            var objetoSerializado = HttpContext.Session.GetString("NuevoPedidoExpres") as string;
            if (objetoSerializado != null)
            {
                PedidoExpresViewModel modelo = JsonConvert.DeserializeObject<PedidoExpresViewModel>(objetoSerializado);

                return modelo; // Enviando el objeto como ViewModel a la vista
            }
            return null;
        }
        private void SetPedidoExpresEnSesion(PedidoExpresViewModel modelo)
        {
            string objetoSerializado = JsonConvert.SerializeObject(modelo);
            HttpContext.Session.SetString("NuevoPedidoExpres", objetoSerializado);
        }
        #endregion

        private void AñadirLinea(PedidoExpresViewModel modelo, PedidoExpresViewModel modeloSesion, ArticuloDTO articulo)
        {
            //actualiza el stock
            LineaPedidoDTO? articuloEnLinea = modeloSesion.Lineas.FirstOrDefault(l => l.ArticuloId == articulo.ArticuloId);

            if (articuloEnLinea != null)
            {
                int cantidadNueva = articuloEnLinea.Cantidad + modelo.Cantidad;

                if (articulo.Stock > cantidadNueva)
                {
                    articuloEnLinea.Cantidad = cantidadNueva; // Actualiza la cantidad si no supera el stock
                }
                else if (articulo.Stock - articuloEnLinea.Cantidad == 0)
                {
                    ViewBag.ErrorLinea = $"Hay {articulo.Stock} en stock, has llegado al máximo.";
                }
                else
                {
                    int stockRestante = articulo.Stock - articuloEnLinea.Cantidad;
                    ViewBag.ErrorLinea = $"No se puede añadir la cantidad deseada. Hay {articulo.Stock} en stock, solo podés añadir {stockRestante} más.";
                }
            }
            //sino comprueba que haya stock y crea nueva linea
            else
            {
                if (modelo.Cantidad <= articulo.Stock)
                {
                    modeloSesion.Lineas.Add(new LineaPedidoDTO
                    {
                        ArticuloId = modelo.ArticuloId,
                        Cantidad = modelo.Cantidad,
                        Articulo = articulo,
                        PrecioUnitario = articulo.Precio
                    });
                }
                else
                {
                    ViewBag.ErrorLinea = $"No hay suficiente stock para el artículo. Stock disponible: {articulo.Stock}.";
                }
            }
        }
        private void RemoverLinea(int? indice, PedidoExpresViewModel modeloSesion)
        {
            if (indice.HasValue && indice.Value >= 0 && indice.Value < modeloSesion.Lineas.Count)
            {
                modeloSesion.Lineas.RemoveAt(indice.Value);
            }
        }
        #endregion

        [HttpGet]
        public IActionResult CrearPedidoExpres()
        {
            chequeoUsuarioValido();
            var modelo = GetPedidoExpresDeSesion();
            if (modelo == null)
            {
                modelo = new PedidoExpresViewModel
                {
                    Clientes = _servicioCliente.GetClientesPorString(""),
                    Articulos = _servicioArticulo.GetArticulosFiltrados("", 0)
                };
            }

            SetPedidoExpresEnSesion(modelo); // Guardar en sesión en caso de ser nuevo
            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CrearPedidoExpres(PedidoExpresViewModel modelo, int? indiceLinea, string accion)
        {
            var modeloSesion = GetPedidoExpresDeSesion();

            var articulo = _servicioArticulo.GetArticuloPorId(modelo.ArticuloId);
            var cliente = _servicioCliente.GetClientePorId(modelo.ClienteId);

            if (cliente != null)
            {
                modeloSesion.Cliente = cliente;
                modeloSesion.ClienteId = modelo.ClienteId;

                switch (accion)
                {
                    case "AñadirLinea":
                        if (articulo != null)
                            AñadirLinea(modelo, modeloSesion, articulo);
                        else
                            ViewBag.ErrorLinea = "Debes seleccionar un artículo";
                        break;

                    case "RemoverLinea":
                        RemoverLinea(indiceLinea, modeloSesion);
                        break;

                    case "CrearPedido":
                        if (modeloSesion.Lineas.Count == 0)
                        {
                            ViewBag.ErrorPedido = "Debes agregar al menos una línea.";
                        }
                        else
                        {
                            try
                            {
                                //elimino el cliente y los articulos despues de calcular el precio total
                                var modeloDTO = modeloSesion.CrearDTO();
                                modeloDTO.Total += modeloSesion.Recargo;
                                modeloDTO.Cliente = null;
                                foreach (var linea in modeloDTO.Lineas)
                                {
                                    linea.Articulo = null;
                                }
                                //Chequear que se cumple la validacion de fecha 

                                _servicioPedidoExpres.Crear(modeloDTO);
                                HttpContext.Session.Remove("NuevoPedido");
                                return RedirectToAction("Clientes", "Usuario");
                            }
                            catch (Exception ex)
                            {
                                ViewBag.ErrorPedido = ex.Message;
                                return View(modeloSesion);
                            }
                        }
                        break;
                }
                modeloSesion.EntregaMismoDia = modelo.EntregaMismoDia;
                modeloSesion.FechaEntregaPrometida = modelo.FechaEntregaPrometida;
                modeloSesion.Total = Math.Round(modeloSesion.Lineas.Sum(l => l.Articulo.Precio * l.Cantidad), 2);
                modeloSesion.Recargo = Math.Round(PedidoExpres.CalcularRecargo(modeloSesion.Total, modeloSesion.EntregaMismoDia), 2);
                SetPedidoExpresEnSesion(modeloSesion);
            } else if (modeloSesion.EntregaMismoDia != modelo.EntregaMismoDia) {
                modeloSesion.EntregaMismoDia = modelo.EntregaMismoDia;
                SetPedidoExpresEnSesion(modeloSesion);
            }
            else
            {
                ViewBag.ErrorLinea = "Debes seleccionar un cliente.";
            }
            return View(modeloSesion);
        }
    }
    #endregion
}

