using Microsoft.AspNetCore.Mvc;
using RestauranteEnHawai.LogicaDeNegocio;
using RestauranteEnHawai.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestauranteEnHawai.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteApiController : ControllerBase
    {
        private readonly BSClientes ServicioClientes = new();

        // GET: api/<ClienteApiController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this.ServicioClientes.ObtenerTodos());
        }

        // GET api/<ClienteApiController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Cliente? cliente = this.ServicioClientes.Obtener(id);
            if(cliente != null)
            {
                return Ok(cliente);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<ClienteApiController>
        [HttpPost]
        public void Post([FromBody] Cliente cliente)
        {
            this.ServicioClientes.Agregar(cliente);
        }

        // PUT api/<ClienteApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Cliente cliente)
        {
            cliente.Id = id;
            this.ServicioClientes.Actualizar(cliente);
        }

        // DELETE api/<ClienteApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Cliente? cliente = this.ServicioClientes.Obtener(id);
            if(cliente != null)
            {
                this.ServicioClientes.Eliminar(cliente);
            }
        }
    }
}
