using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestauranteEnHawai.LogicaDeNegocio;
using RestauranteEnHawai.Models;
using System.Text;

namespace RestauranteEnHawai.Controllers
{
    public class ReservaController : ConsumidorRestController
    {
        //private readonly BSReserva ServicioReserva = new();
        //public async Task<ActionResult> Index()
        //{
        //    string respuestaJson = await clienteHttp.GetStringAsync("api/PlatoApi");
        //    return View(JsonConvert.DeserializeObject<List<Plato>>(respuestaJson));
        //}

        // GET: ReservaController
        public async Task<ActionResult> Index()
        {
            string respuestaJson = await clienteHttp.GetStringAsync("api/ReservaApi");
            return View(JsonConvert.DeserializeObject<List<Reserva>>(respuestaJson));
        }

        // GET: ReservaController/Details/5
        public async Task<ActionResult> Details(int numeroReserva)
        {
            string respuestaJson = await clienteHttp.GetStringAsync("api/ReservaApi/"+ numeroReserva);
            Reserva? reserva = JsonConvert.DeserializeObject<Reserva>(respuestaJson);
            if (reserva != null)
            {
                return View(reserva);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
            
        }

        // GET: ReservaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReservaeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                Reserva reserva = new();
                reserva.FechaHoraReserva = DateTime.Parse(collection["FechaHoraReserva"]);
                reserva.NombreCliente = collection["NombreCliente"];
                reserva.CantidadPersonas = int.Parse(collection["CantidadPersonas"]);
                if(reserva.CantidadPersonas <= 0)
                {
                    ModelState.AddModelError("CantidadPersonas", "La cantidad de personas tiene que ser mayor a cero.");
                }
                if(ModelState.ErrorCount == 0)
                {
                    var contenido = new StringContent(JsonConvert.SerializeObject(reserva), Encoding.UTF8, "application/json");
                    await this.clienteHttp.PostAsync("api/ReservaApi", contenido);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(reserva);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: ReservaController/Edit/5
        public async Task<ActionResult> Edit(int numeroReserva)
        {
            string respuestaJson = await clienteHttp.GetStringAsync("api/ReservaApi/" + numeroReserva);
            Reserva? reserva = JsonConvert.DeserializeObject<Reserva>(respuestaJson);
            if (reserva != null)
            {
                return View(reserva);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
            
        }

        // POST: ReservaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int numeroReserva, IFormCollection collection)
        {
            try
            {
                Reserva reserva = new();
                reserva.NumeroDeReserva = numeroReserva;
                reserva.FechaHoraReserva = DateTime.Parse(collection["FechaHoraReserva"]);
                reserva.NombreCliente = collection["NombreCliente"];
                reserva.CantidadPersonas = int.Parse(collection["CantidadPersonas"]);
                if (reserva.CantidadPersonas <= 0)
                {
                    ModelState.AddModelError("CantidadPersonas", "La cantidad de personas tiene que ser mayor a cero.");
                }
                if (ModelState.ErrorCount == 0)
                {
                    var contenido = new StringContent(JsonConvert.SerializeObject(reserva), Encoding.UTF8, "application/json");
                    await this.clienteHttp.PutAsync("api/ReservaApi/" + numeroReserva, contenido);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(reserva);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: ReservaeController/Delete/5
        public async Task<ActionResult> Delete(int numeroReserva)
        {
            string respuestaJson = await clienteHttp.GetStringAsync("api/ReservaApi/" + numeroReserva);
            Reserva? reserva = JsonConvert.DeserializeObject<Reserva>(respuestaJson);
            if (reserva != null)
            {
                return View(reserva);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: ReservaeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int numeroReserva, IFormCollection collection)
        {
            try
            {
                string respuestaJson = await clienteHttp.GetStringAsync("api/ReservaApi/" + numeroReserva);
                Reserva? reserva = JsonConvert.DeserializeObject<Reserva>(respuestaJson);
                if (reserva != null)
                {
                    await this.clienteHttp.DeleteAsync("api/ReservaApi/" + numeroReserva);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
