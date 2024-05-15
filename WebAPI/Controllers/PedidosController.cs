using Microsoft.AspNetCore.Http;
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
            try
            {
                var pedidosAnulados = _servicioPedido.GetPedidosAnulados();
                return Ok(pedidosAnulados);
            }
            catch (Exception ex)
            {
                // Log the exception details here
                return StatusCode(500, "Por favor, intentá más tarde.");
            }
        }
    }
}
