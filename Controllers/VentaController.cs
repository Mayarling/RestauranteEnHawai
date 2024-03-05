using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestauranteEnHawai.LogicaDeNegocio;
using RestauranteEnHawai.Models;
using System.Text;

namespace RestauranteEnHawai.Controllers
{
    public class VentaController : ConsumidorRestController
    {
        // GET: VentaController
        public async Task<ActionResult> Index()
        {
            string respuestaJson = await clienteHttp.GetStringAsync("api/VentaApi");
            return View(JsonConvert.DeserializeObject<List<Venta>>(respuestaJson));
        }

        // GET: VentaController/Details/5
        public async Task<ActionResult> Details(int numeroOrden)
        {
            string respuestaJson = await clienteHttp.GetStringAsync("api/VentaApi/"+ numeroOrden);
            Venta? venta = JsonConvert.DeserializeObject<Venta>(respuestaJson);
            if (venta != null)
            {
                return View(venta);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: VentaController/Create
        public async Task<ActionResult> Create()
        {
            string respuestaJson = await clienteHttp.GetStringAsync("api/PlatoApi");
            ViewData["Platos"] = JsonConvert.DeserializeObject<List<Plato>>(respuestaJson);
            return View();
        }

        // POST: VentaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                Venta venta = new Venta();
                venta.FechaHora = DateTime.Parse(collection["FechaHora"]);
                int idPlato = int.Parse(collection["PlatoVendido"]);
                string respuestaJson = await clienteHttp.GetStringAsync("api/PlatoApi/"+ idPlato);
                venta.PlatoVendido = JsonConvert.DeserializeObject<Plato>(respuestaJson);
                venta.CantidadVendida = int.Parse(collection["CantidadVendida"]);
                if(venta.CantidadVendida <= 0)
                {
                    ModelState.AddModelError("CantidadVendida", "La cantidad vendida debe ser un número mayor a cero.");
                }
                if(ModelState.ErrorCount == 0)
                {
                    var contenido = new StringContent(JsonConvert.SerializeObject(venta), Encoding.UTF8, "application/json");
                    await this.clienteHttp.PostAsync("api/VentaApi", contenido);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    respuestaJson = await clienteHttp.GetStringAsync("api/PlatoApi");
                    ViewData["Platos"] = JsonConvert.DeserializeObject<List<Plato>>(respuestaJson);
                    return View(venta);
                }
            }
            catch
            {
                string respuestaJson = await clienteHttp.GetStringAsync("api/PlatoApi");
                ViewData["Platos"] = JsonConvert.DeserializeObject<List<Plato>>(respuestaJson);
                return View();
            }
        }

        // GET: VentaController/Edit/5
        public async Task<ActionResult> Edit(int numeroOrden)
        {
            string respuestaJson = await clienteHttp.GetStringAsync("api/VentaApi/" + numeroOrden);
            Venta? venta = JsonConvert.DeserializeObject<Venta>(respuestaJson);
            if (venta != null)
            {
                respuestaJson = await clienteHttp.GetStringAsync("api/PlatoApi");
                ViewData["Platos"] = JsonConvert.DeserializeObject<List<Plato>>(respuestaJson);
                return View(venta);
            } 
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: VentaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int numeroOrden, IFormCollection collection)
        {
            try
            {
                Venta venta = new Venta();
                venta.NumeroOrden = numeroOrden;
                venta.FechaHora = DateTime.Parse(collection["FechaHora"]);
                int idPlato = int.Parse(collection["PlatoVendido"]);
                string respuestaJson = await clienteHttp.GetStringAsync("api/PlatoApi/" + idPlato);
                venta.PlatoVendido = JsonConvert.DeserializeObject<Plato>(respuestaJson);
                venta.CantidadVendida = int.Parse(collection["CantidadVendida"]);
                if (venta.CantidadVendida <= 0)
                {
                    ModelState.AddModelError("CantidadVendida", "La cantidad vendida debe ser un número mayor a cero.");
                }
                if (ModelState.ErrorCount == 0)
                {
                    var contenido = new StringContent(JsonConvert.SerializeObject(venta), Encoding.UTF8, "application/json");
                    await this.clienteHttp.PostAsync("api/VentaApi", contenido);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    respuestaJson = await clienteHttp.GetStringAsync("api/PlatoApi");
                    ViewData["Platos"] = JsonConvert.DeserializeObject<List<Plato>>(respuestaJson);
                    return View(venta);
                }
            }
            catch
            {
                var respuestaJson = await clienteHttp.GetStringAsync("api/PlatoApi");
                ViewData["Platos"] = JsonConvert.DeserializeObject<List<Plato>>(respuestaJson);
                return View();
            }
        }

        // GET: VentaController/Delete/5
        public async Task<ActionResult> Delete(int numeroOrden)
        {
            string respuestaJson = await clienteHttp.GetStringAsync("api/VentaApi/" + numeroOrden);
            Venta? venta = JsonConvert.DeserializeObject<Venta>(respuestaJson);
            if (venta != null)
            {
                return View(venta);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: VentaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int numeroOrden, IFormCollection collection)
        {
            try
            {
                string respuestaJson = await clienteHttp.GetStringAsync("api/VentaApi/" + numeroOrden);
                Venta? venta = JsonConvert.DeserializeObject<Venta>(respuestaJson);
                if (venta != null)
                {
                    await clienteHttp.DeleteAsync("api/VentaApi/" + numeroOrden);
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
