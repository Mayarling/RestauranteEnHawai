using Microsoft.AspNetCore.Mvc;
using RestauranteEnHawai.LogicaDeNegocio;
using RestauranteEnHawai.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestauranteEnHawai.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaApiController : ControllerBase
    {
        private readonly BSReserva ServicioReserva = new();

        // GET: api/<ReservaApiController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(ServicioReserva.ObtenerTodos());
        }

        // GET api/<ReservaApiController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int numeroReserva)
        {
            Reserva? reserva = ServicioReserva.Obtener(numeroReserva);
            if (reserva != null)
            {
                return Ok(reserva);
            }
            else
            {
                return NotFound();
            }
        }
        // POST api/<VentaApiController>
        [HttpPost]
        public void Post([FromBody] Reserva reserva)
        {
            ServicioReserva.Agregar(reserva);
        }


        // PUT api/<ReservaApiController>/5
        [HttpPut("{id}")]
        public void Put(int numeroReserva, [FromBody] Reserva reserva)
        {
            reserva.NumeroDeReserva = numeroReserva;
            ServicioReserva.Actualizar(reserva);
        }

        // DELETE api/<ReservaApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int numeroReserva)
        {
            Reserva? reserva = ServicioReserva.Obtener(numeroReserva);
            if(reserva != null)
            {
                ServicioReserva.Delete(reserva);
            }
        }
    }
}
