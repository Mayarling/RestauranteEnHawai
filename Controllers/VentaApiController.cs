using Microsoft.AspNetCore.Mvc;
using RestauranteEnHawai.LogicaDeNegocio;
using RestauranteEnHawai.Models;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestauranteEnHawai.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaApiController : ControllerBase
    {
        private readonly BSVentas ServicioVenta = new();

        // GET: api/<VentaApiController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(ServicioVenta.ObtenerVentas());
        }

        // GET api/<VentaApiController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Venta? venta = this.ServicioVenta.Obtener(id);
            if(venta != null)
            {
                return Ok(venta);
            } else
            {
                return NotFound();
            }
        }

        // POST api/<VentaApiController>
        [HttpPost]
        public void Post([FromBody] Venta venta)
        {
            this.ServicioVenta.Agregar(venta);
        }

        // PUT api/<VentaApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Venta venta)
        {
            venta.NumeroOrden = id;
            this.ServicioVenta.Actualizar(venta);
        }

        // DELETE api/<VentaApiController>/5
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Método para borrar una venta.", Description = "Método que borra una venta dado un ID.")]
        public void Delete(int id)
        {
            Venta? venta = this.ServicioVenta.Obtener(id);
            if(venta != null)
            {
                this.ServicioVenta.Delete(venta);
            }
        }
    }
}
