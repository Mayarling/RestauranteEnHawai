using Microsoft.AspNetCore.Mvc;

namespace RestauranteEnHawai.Controllers
{
    public class ConsumidorRestController : Controller
    {
        protected readonly Uri endpoint = new("http://localhost:5152");
        protected readonly HttpClient clienteHttp = new HttpClient();

        public ConsumidorRestController()
        {
            clienteHttp.BaseAddress = endpoint;
        }
    }
}
