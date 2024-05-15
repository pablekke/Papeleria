using Microsoft.AspNetCore.Mvc;
using Servicios;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticulosController : ControllerBase
    {
        private readonly IServicioArticulo _servicioArticulo;

        public ArticulosController(IServicioArticulo servicioArticulo)
        {
            _servicioArticulo = servicioArticulo;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string nombre = "", double monto = 0)
        {
            return Ok(_servicioArticulo.GetArticulosFiltrados(nombre, monto));
        }
        [HttpGet]
        public IActionResult GetById([FromQuery] int id)
        {
            return Ok(_servicioArticulo.GetArticuloPorId(id));
        }
    }
}
