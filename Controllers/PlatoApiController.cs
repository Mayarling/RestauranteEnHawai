using Microsoft.AspNetCore.Mvc;
using RestauranteEnHawai.Models;
using RestauranteEnHawai.LogicaDeNegocio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestauranteEnHawai.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatoApiController : ControllerBase
    {
        private readonly BSPlato ServicioPlato = new();

        // GET: api/<PlatoApiController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(ServicioPlato.ObtenerTodos());
        }

        // GET api/<PlatoApiController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Plato? plato = ServicioPlato.Obtener(id);
            if (plato != null)
            {
                return Ok(plato);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<PlatoApiController>
        [HttpPost]
        public void Post([FromBody] Plato plato)
        {
            ServicioPlato.Agregar(plato);
        }

        // PUT api/<PlatoApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Plato plato)
        {
            plato.Id = id;
            ServicioPlato.Actualizar(plato);
        }

        // DELETE api/<PlatoApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Plato? plato = ServicioPlato.Obtener(id);
            if (plato != null)
            {
                ServicioPlato.Delete(plato);
            }
        }
    }
}
