using Microsoft.AspNetCore.Mvc;
using Servicios;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IServicioPedido _servicioPedido;

        public PedidosController(IServicioPedido servicioPedido)
        {
            _servicioPedido = servicioPedido;
        }

        [HttpGet]
        public IActionResult GetPedidosAnulados()
        {
            var pedidosAnulados = _servicioPedido.GetPedidosAnulados();
            return Ok(pedidosAnulados);
        }
    }
}
